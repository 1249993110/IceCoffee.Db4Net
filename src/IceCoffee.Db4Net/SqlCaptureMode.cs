namespace IceCoffee.Db4Net
{
    /// <summary>
    /// Specifies the strategy for including SQL statements when throwing exceptions.
    /// </summary>
    public enum SqlCaptureMode
    {
        /// <summary>
        /// Include the SQL statement only when a debugger is attached.
        /// </summary>
        OnlyDebuggerAttached,

        /// <summary>
        /// Never include the SQL statement in the exception.
        /// </summary>
        Never,

        /// <summary>
        /// Always include the SQL statement in the exception.
        /// </summary>
        Always
    }
}
