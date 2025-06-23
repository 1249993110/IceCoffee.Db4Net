namespace IceCoffee.Db4Net.Core.FilterDefinitions
{
    internal sealed class AndFilterDefinition : FilterDefinition
    {
        private readonly IEnumerable<FilterDefinition> _filters;

        public AndFilterDefinition(IEnumerable<FilterDefinition> filters)
        {
            _filters = filters;
        }

        internal override string Render(ParameterBuilder parameterBuilder)
        {
            return string.Join(" AND ", _filters.Select(i => i is OrFilterDefinition ? "(" + i.Render(parameterBuilder) + ")" : i.Render(parameterBuilder)));
        }
    }
}
