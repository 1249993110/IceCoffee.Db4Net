using IceCoffee.Db4Net.Core.Constants;
using IceCoffee.Db4Net.Core.PropertyDefinitions;
using System.Collections;
using System.Linq.Expressions;

namespace IceCoffee.Db4Net.Core.FilterDefinitions
{
    public sealed class FilterDefinitionBuilder<TEntity> : FilterDefinitionBuilder
    {
        public static new FilterDefinitionBuilder<TEntity> Default { get; } = new FilterDefinitionBuilder<TEntity>();

        #region Operators
        public FilterDefinition Eq(PropertyDefinition<TEntity> prop, object value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.Equal, prop, value);
        }
        public FilterDefinition Eq<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.Equal, new ExpressionPropertyDefinition<TEntity, TProp>(prop), value);
        }
        public FilterDefinition Ne(PropertyDefinition<TEntity> prop, object value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.NotEqual, prop, value);
        }
        public FilterDefinition Ne<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.NotEqual, new ExpressionPropertyDefinition<TEntity, TProp>(prop), value);
        }
        public FilterDefinition Gt(PropertyDefinition<TEntity> prop, object value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.GreaterThan, prop, value);
        }
        public FilterDefinition Gt<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.GreaterThan, new ExpressionPropertyDefinition<TEntity, TProp>(prop), value);
        }
        public FilterDefinition Gte(PropertyDefinition<TEntity> prop, object value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.GreaterThanOrEqual, prop, value);
        }
        public FilterDefinition Gte<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.GreaterThanOrEqual, new ExpressionPropertyDefinition<TEntity, TProp>(prop), value);
        }
        public FilterDefinition Lt(PropertyDefinition<TEntity> prop, object value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.LessThan, prop, value);
        }
        public FilterDefinition Lt<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.LessThan, new ExpressionPropertyDefinition<TEntity, TProp>(prop), value);
        }
        public FilterDefinition Lte(PropertyDefinition<TEntity> prop, object value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.LessThanOrEqual, prop, value);
        }
        public FilterDefinition Lte<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.LessThanOrEqual, new ExpressionPropertyDefinition<TEntity, TProp>(prop), value);
        }
        public FilterDefinition Like(PropertyDefinition<TEntity> prop, object value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.Like, prop, value);
        }
        public FilterDefinition Like<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.Like, new ExpressionPropertyDefinition<TEntity, TProp>(prop), value);
        }
        public FilterDefinition NotLike(PropertyDefinition<TEntity> prop, object value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.NotLike, prop, value);
        }
        public FilterDefinition NotLike<TProp>(Expression<Func<TEntity, TProp>> prop, TProp value)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.NotLike, new ExpressionPropertyDefinition<TEntity, TProp>(prop), value);
        }
        public FilterDefinition In(PropertyDefinition<TEntity> prop, IEnumerable values)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.In, prop, values);
        }
        public FilterDefinition In<TProp>(Expression<Func<TEntity, TProp>> prop, IEnumerable<TProp> values)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.In, new ExpressionPropertyDefinition<TEntity, TProp>(prop), values);
        }
        public FilterDefinition NotIn(PropertyDefinition<TEntity> prop, IEnumerable values)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.NotIn, prop, values);
        }
        public FilterDefinition NotIn<TProp>(Expression<Func<TEntity, TProp>> prop, IEnumerable<TProp> values)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.NotIn, new ExpressionPropertyDefinition<TEntity, TProp>(prop), values);
        }
        public FilterDefinition IsNull(PropertyDefinition<TEntity> prop)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.IsNull, prop, null);
        }
        public FilterDefinition IsNull<TProp>(Expression<Func<TEntity, TProp>> prop)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.IsNull, new ExpressionPropertyDefinition<TEntity, TProp>(prop), null);
        }
        public FilterDefinition IsNotNull(PropertyDefinition<TEntity> prop)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.IsNotNull, prop, null);
        }
        public FilterDefinition IsNotNull<TProp>(Expression<Func<TEntity, TProp>> prop)
        {
            return new OperatorFilterDefinition<TEntity>(OperatorType.IsNotNull, new ExpressionPropertyDefinition<TEntity, TProp>(prop), null);
        }
        public FilterDefinition Between(PropertyDefinition<TEntity> prop, object minValue, object maxValue)
        {
            return And(
                new OperatorFilterDefinition<TEntity>(OperatorType.GreaterThanOrEqual, prop, minValue),
                new OperatorFilterDefinition<TEntity>(OperatorType.LessThanOrEqual, prop, maxValue)
            );
        }
        public FilterDefinition Between<TProp>(Expression<Func<TEntity, TProp>> prop, TProp minValue, TProp maxValue)
        {
            return And(
                new OperatorFilterDefinition<TEntity>(OperatorType.GreaterThanOrEqual, new ExpressionPropertyDefinition<TEntity, TProp>(prop), minValue),
                new OperatorFilterDefinition<TEntity>(OperatorType.LessThanOrEqual, new ExpressionPropertyDefinition<TEntity, TProp>(prop), maxValue)
            );
        }
        public FilterDefinition NotBetween(PropertyDefinition<TEntity> prop, object minValue, object maxValue)
        {
            return Or(
                new OperatorFilterDefinition<TEntity>(OperatorType.LessThan, prop, minValue),
                new OperatorFilterDefinition<TEntity>(OperatorType.GreaterThan, prop, maxValue)
            );
        }
        public FilterDefinition NotBetween<TProp>(Expression<Func<TEntity, TProp>> prop, TProp minValue, TProp maxValue)
        {
            return Or(
                new OperatorFilterDefinition<TEntity>(OperatorType.LessThan, new ExpressionPropertyDefinition<TEntity, TProp>(prop), minValue),
                new OperatorFilterDefinition<TEntity>(OperatorType.GreaterThan, new ExpressionPropertyDefinition<TEntity, TProp>(prop), maxValue)
            );
        }
        #endregion
    }
}
