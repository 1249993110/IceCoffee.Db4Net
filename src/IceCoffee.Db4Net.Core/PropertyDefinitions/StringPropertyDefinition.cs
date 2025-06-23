using IceCoffee.Db4Net.Core.SqlAdapters;
using IceCoffee.Db4Net.Core.SqlBuilders;

namespace IceCoffee.Db4Net.Core.PropertyDefinitions
{
    internal sealed class StringPropertyDefinition<TEntity> : PropertyDefinition<TEntity>
    {
        private readonly string _propertyName;
        public override string PropertyName => _propertyName;

        public StringPropertyDefinition(string propertyName)
        {
            _propertyName = propertyName;
        }

        internal override string Render(ISqlAdapter sqlAdapter)
        {
            return sqlAdapter.Quote(SqlBuilder<TEntity>.GetFieldNameByProperty(_propertyName));
        }
    }
}
