namespace IceCoffee.Db4Net
{
    /// <summary>
    /// Represents an exception that is thrown when a SQL execution error occurs.
    /// </summary>
    public class SqlExecuteException : Exception
    {
        /// <summary>
        /// Gets or sets the SQL statement that was executed.
        /// </summary>
        public string? Sql { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlExecuteException"/> class
        /// with a specified error message and optionally an inner exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The inner exception reference.</param>
        internal SqlExecuteException(string? message, Exception? inner) : base(null, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlExecuteException"/> class
        /// with a specified error message, optionally an inner exception, and the corresponding SQL statement.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The inner exception reference.</param>
        /// <param name="sql">The SQL statement that caused the exception.</param>
        internal SqlExecuteException(string? message, Exception? inner, string? sql) : base(message, inner)
        {
            Sql = sql;
        }
    }
}
