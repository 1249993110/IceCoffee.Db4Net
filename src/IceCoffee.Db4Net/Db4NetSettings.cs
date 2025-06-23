namespace IceCoffee.Db4Net
{
    /// <summary>
    /// Represents the settings for Db4Net.
    /// </summary>
    public class Db4NetSettings
    {
        /// <summary>
        /// Gets or sets the SQL capture mode.
        /// The default value is <see cref="Db4Net.SqlCaptureMode.OnlyDebuggerAttached"/>.
        /// </summary>
        public SqlCaptureMode SqlCaptureMode { get; set; } = SqlCaptureMode.OnlyDebuggerAttached;

        /// <summary>
        /// Gets or sets the prefix for automatically generated parameter names.
        /// The default value is "p".
        /// </summary>
        public string ParameterNamePrefix { get; set; } = "p";

        /// <summary>
        /// Gets or sets a value indicating whether to reuse DbParameter objects.
        /// The default value is true.
        /// </summary>
        public bool ReuseParameters { get; set; } = true;
    }
}
