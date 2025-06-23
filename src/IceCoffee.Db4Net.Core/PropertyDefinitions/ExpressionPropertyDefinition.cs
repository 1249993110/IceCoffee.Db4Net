using IceCoffee.Db4Net.Core.Extensions;
using IceCoffee.Db4Net.Core.SqlAdapters;
using System.Linq.Expressions;

namespace IceCoffee.Db4Net.Core.PropertyDefinitions
{
    internal sealed class ExpressionPropertyDefinition<TEntity, TProp> : PropertyDefinition<TEntity, TProp>
    {
        private readonly string _propertyName;
        public override string PropertyName => _propertyName;
        public ExpressionPropertyDefinition(Expression<Func<TEntity, TProp>> expression)
        {
            _propertyName = expression.GetPropertyName();
        }

        internal override string Render(ISqlAdapter sqlAdapter)
        {
            return new StringPropertyDefinition<TEntity>(_propertyName).Render(sqlAdapter);
        }
    }
}
