using IceCoffee.Db4Net.Core.FilterDefinitions;
using IceCoffee.Db4Net.Core.OptionalAttributes;
using IceCoffee.Db4Net.Core.SqlAdapters;
using System.Collections;
using System.Reflection;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    /// <summary>
    /// Provides methods to build up SQL, adding up parameters and conditions to the query and generate the final SQL statement.
    /// </summary>
    public abstract class SqlBuilder<TEntity> : SqlBuilder
    {
        public static new FilterDefinitionBuilder<TEntity> Filter => FilterDefinitionBuilder<TEntity>.Default;
        public static string DefaultDatabaseName { get; private set; }
        public static string DefaultTableName { get; private set; }

        protected static string DefaultSelection { get; private set; } = string.Empty;
        protected static (string Columns, string Parameters) DefaultInsertClause { get; private set; }
        protected static string DefaultUpdateClause { get; private set; } = string.Empty;

        class PropsTo
        {
            public List<string> Select { get; set; } = new List<string>();
            public List<string> Insert { get; set; } = new List<string>();
            public List<string> Update { get; set; } = new List<string>();
        }
        private static PropsTo? _propsTo;

        private static readonly Dictionary<string, string> _propToFieldMap = new Dictionary<string, string>();
        private static readonly List<string>? _uniqueKeys;
        private static List<string> UniqueKeys => _uniqueKeys ?? throw new Exception("No unique key is identified.");

        static SqlBuilder()
        {
            var entityType = typeof(TEntity);

            if(typeof(IEnumerable).IsAssignableFrom(entityType))
            {
                throw new InvalidOperationException($"The type '{entityType.Name}' is not a valid entity type for SqlBuilder. It should not be a collection or array type.");
            }

            var databaseAttribute = entityType.GetCustomAttribute<DatabaseAttribute>(true);
            if (databaseAttribute != null)
            {
                DefaultDatabaseName = databaseAttribute.Name;
            }
            else
            {
                databaseAttribute = entityType.Assembly.GetCustomAttribute<DatabaseAttribute>();
                if (databaseAttribute != null)
                {
                    DefaultDatabaseName = databaseAttribute.Name;
                }
                else
                {
                    DefaultDatabaseName = string.Empty;
                }
            }

            var tableAttribute = entityType.GetCustomAttribute<TableAttribute>(true);
            DefaultTableName = tableAttribute?.Name ?? entityType.Name;
            
            var properties = entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanWrite && p.GetCustomAttribute<NotMappedAttribute>(true) == null);

            if (properties.Any() == false)
            {
                throw new InvalidOperationException($"Entity '{entityType.Name}' does not have any valid properties to map to database fields. Ensure that the properties are public, writable, and not marked with [NotMapped].");
            }

            _propsTo = new PropsTo();
            foreach (var prop in properties)
            {
                string propertyName = prop.Name;
                string fieldName = prop.GetCustomAttribute<ColumnAttribute>(true)?.Name ?? propertyName;

                _propToFieldMap.Add(propertyName, fieldName);

                if (prop.GetCustomAttribute<IgnoreSelectAttribute>(true) == null)
                {
                    _propsTo.Select.Add(propertyName);
                }

                if (prop.GetCustomAttribute<DatabaseGeneratedAttribute>(true) == null)
                {
                    if (prop.GetCustomAttribute<IgnoreInsertAttribute>(true) == null)
                    {
                        _propsTo.Insert.Add(propertyName);
                    }

                    if (prop.GetCustomAttribute<IgnoreUpdateAttribute>(true) == null)
                    {
                        _propsTo.Update.Add(propertyName);
                    }
                }
            }

            var primaryKeyProps = properties.Where(p => p.GetCustomAttribute<UniqueKeyAttribute>(true) != null);
            if (primaryKeyProps.Any())
            {
                _uniqueKeys = new List<string>();
                foreach (var prop in primaryKeyProps)
                {
                    _uniqueKeys.Add(prop.Name);
                }
            }
        }
        internal static string GetUniqueConstraint(ISqlAdapter sqlAdapter)
        {
            string unique;
            if (UniqueKeys.Count == 1)
            {
                unique = $"{sqlAdapter.Quote(_propToFieldMap[UniqueKeys[0]])} = {sqlAdapter.Parameter(UniqueKeys[0])}";
            }
            else
            {
                unique = string.Join(" AND ", UniqueKeys.Select(i => $"{sqlAdapter.Quote(_propToFieldMap[i])} = {sqlAdapter.Parameter(i)}"));
            }
            return unique;
        }
        internal static string GetSingleUniqueKey()
        {
            if (UniqueKeys.Count > 1)
            {
                throw new InvalidOperationException("The entity must have a single unique key to use this method.");
            }

            return UniqueKeys[0];
        }

        public SqlBuilder(ISqlAdapter sqlAdapter) : base(sqlAdapter)
        {
            DatabaseName = DefaultDatabaseName;
            if (_propsTo != null)
            {
                DefaultTableName = TryQuote(DefaultTableName);
                DefaultSelection = string.Join(", ", _propsTo.Select.Select(propertyName =>
                {
                    string fieldName = _propToFieldMap[propertyName];
                    if(propertyName == fieldName)
                    {
                        return TryQuote(fieldName);
                    }
                    return TryQuote(fieldName) + " AS " + TryQuote(propertyName);
                }));
                DefaultInsertClause = (string.Join(", ", _propsTo.Insert.Select(i => TryQuote(_propToFieldMap[i]))), string.Join(", ", _propsTo.Insert.Select(sqlAdapter.Parameter)));
                DefaultUpdateClause = string.Join(", ", _propsTo.Update.Select(i => $"{TryQuote(_propToFieldMap[i])} = {sqlAdapter.Parameter(i)}"));
                _propsTo = null;
            }
        }

        internal static string GetFieldNameByProperty(string propertyName)
        {
            if (_propToFieldMap.TryGetValue(propertyName, out string? fieldName))
            {
                return fieldName;
            }

            throw new ArgumentException($"Property '{propertyName}' is not a valid field for entity '{typeof(TEntity).Name}'.");
        }
    }
}
