namespace IceCoffee.Db4Net.SqliteTypeHandlers
{
    /// <summary>
    /// Provides functionality for handling <see cref="TimeSpan"/> values in SQLite operations.
    /// </summary>
    /// <remarks>This class is designed to parse <see cref="TimeSpan"/> values from SQLite database objects.
    /// It converts the database value, typically stored as a string, into a <see cref="TimeSpan"/> instance.</remarks>
    public class TimeSpanHandler : SqliteTypeHandler<TimeSpan>
    {
        /// <summary>
        /// Converts the specified object to a <see cref="TimeSpan"/> instance.
        /// </summary>
        /// <param name="value">The object to convert. Must be a string representation of a <see cref="TimeSpan"/>.</param>
        /// <returns>A <see cref="TimeSpan"/> instance that corresponds to the string representation provided in <paramref
        /// name="value"/>.</returns>
        public override TimeSpan Parse(object value)
            => TimeSpan.Parse((string)value);
    }
}