namespace IceCoffee.Db4Net.Core.FilterDefinitions
{
    public abstract class FilterDefinition
    {
        internal abstract string Render(ParameterBuilder parameterBuilder);

        /// <summary>
        /// Implements the operator &amp;.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static FilterDefinition operator &(FilterDefinition lhs, FilterDefinition rhs)
        {
            return new AndFilterDefinition(new[] { lhs, rhs });
        }

        /// <summary>
        /// Implements the operator |.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static FilterDefinition operator |(FilterDefinition lhs, FilterDefinition rhs)
        {
            return new OrFilterDefinition(new[] { lhs, rhs });
        }

        /// <summary>
        /// Implements the operator !.
        /// </summary>
        /// <param name="op">The op.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static FilterDefinition operator !(FilterDefinition op)
        {
            return new NotFilterDefinition(op);
        }

        public static implicit operator FilterDefinition(FormattableString formattableString)
        {
            return new FormattableStringFilterDefinition(formattableString);
        }
    }
}
