using IceCoffee.Db4Net.Core.SqlAdapters;
using System.Linq.Expressions;

namespace IceCoffee.Db4Net.Core.PropertyDefinitions
{
    public abstract class PropertyDefinition<TEntity>
    {
        public abstract string PropertyName { get; }

        internal abstract string Render(ISqlAdapter sqlAdapter);

        public static implicit operator PropertyDefinition<TEntity>(string propertyName)
        {
            return new StringPropertyDefinition<TEntity>(propertyName);
        }

        public static implicit operator PropertyDefinition<TEntity>(Enum @enum)
        {
            return new StringPropertyDefinition<TEntity>(@enum.ToString());
        }
    }

    public abstract class PropertyDefinition<TEntity, TProp> : PropertyDefinition<TEntity>
    {
        public static implicit operator PropertyDefinition<TEntity, TProp>(Expression<Func<TEntity, TProp>> expression)
        {
            return new ExpressionPropertyDefinition<TEntity, TProp>(expression);
        }
    }
}
