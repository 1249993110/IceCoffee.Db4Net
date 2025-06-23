using IceCoffee.Db4Net.Core.Constants;
using IceCoffee.Db4Net.Core.PropertyDefinitions;

namespace IceCoffee.Db4Net.Core.FilterDefinitions
{
    internal sealed class OperatorFilterDefinition<TEntity> : FilterDefinition
    {
        private readonly OperatorType _operatorType;
        private readonly PropertyDefinition<TEntity> _field;
        private readonly object? _param;

        public OperatorFilterDefinition(OperatorType operatorType, PropertyDefinition<TEntity> field, object? param)
        {
            _operatorType = operatorType;
            _field = field;
            _param = param;
        }

        internal override string Render(ParameterBuilder parameterBuilder)
        {
            var adapter = parameterBuilder.SqlAdapter;
            string field = _field.Render(adapter);
            return _operatorType switch
            {
                OperatorType.Equal => $"{field} = {parameterBuilder.AddNamedParam(_param)}",
                OperatorType.NotEqual => $"{field} <> {parameterBuilder.AddNamedParam(_param)}",
                OperatorType.GreaterThan => $"{field} > {parameterBuilder.AddNamedParam(_param)}",
                OperatorType.GreaterThanOrEqual => $"{field} >= {parameterBuilder.AddNamedParam(_param)}",
                OperatorType.LessThan => $"{field} < {parameterBuilder.AddNamedParam(_param)}",
                OperatorType.LessThanOrEqual => $"{field} <= {parameterBuilder.AddNamedParam(_param)}",
                OperatorType.Like => $"{field} {adapter.Like(parameterBuilder.AddNamedParam(_param))}",
                OperatorType.NotLike => $"{field} NOT {adapter.Like(parameterBuilder.AddNamedParam(_param))}",
                OperatorType.In => $"{field} {adapter.In(parameterBuilder.AddNamedParam(_param))}",
                OperatorType.NotIn => $"{field} NOT {adapter.In(parameterBuilder.AddNamedParam(_param))}",
                OperatorType.IsNull => $"{field} IS NULL",
                OperatorType.IsNotNull => $"{field} IS NOT NULL",
                _ => throw new NotSupportedException($"Operator '{_operatorType}' is not supported."),
            };
        }
    }
}
