namespace IceCoffee.Db4Net.Core.FilterDefinitions
{
    internal class FormattableStringFilterDefinition : FilterDefinition
    {
        private readonly FormattableString _formattableString;

        public FormattableStringFilterDefinition(FormattableString formattableString)
        {
            _formattableString = formattableString;
        }

        internal override string Render(ParameterBuilder parameterBuilder)
        {
            return Utils.ParseFormattableString(_formattableString, parameterBuilder.AddNamedParam);
        }
    }
}
