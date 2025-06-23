namespace IceCoffee.Db4Net.Core.FilterDefinitions
{
    internal sealed class NotFilterDefinition : FilterDefinition
    {
        private readonly FilterDefinition _filter;

        public NotFilterDefinition(FilterDefinition filter)
        {
            _filter = filter;
        }

        internal override string Render(ParameterBuilder parameterBuilder)
        {
            return "NOT (" + _filter.Render(parameterBuilder) + ")";
        }
    }
}
