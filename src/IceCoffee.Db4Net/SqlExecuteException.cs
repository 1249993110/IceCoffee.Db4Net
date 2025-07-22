namespace IceCoffee.Db4Net
{
    /// <summary>
    /// Represents an exception that is thrown when a SQL execution error occurs.
    /// </summary>
    public class SqlExecuteException : Exception
    {
        private const string _defaultMessage = "An error occurred while executing the SQL statement.";

        /// <summary>
        /// Gets or sets the SQL statement that was executed.
        /// </summary>
        public string? Sql { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlExecuteException"/> class
        /// with a specified error message and optionally an inner exception.
        /// </summary>
        /// <param name="inner">The inner exception reference.</param>
        internal SqlExecuteException(Exception? inner) : base(_defaultMessage, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlExecuteException"/> class
        /// with a specified error message, optionally an inner exception, and the corresponding SQL statement.
        /// </summary>
        /// <param name="inner">The inner exception reference.</param>
        /// <param name="sql">The SQL statement that caused the exception.</param>
        internal SqlExecuteException(Exception? inner, string? sql) : base(_defaultMessage, inner)
        {
            Sql = sql;
        }
    }
}
