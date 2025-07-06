namespace IceCoffee.Db4Net.SqliteTypeHandlers
{
    /// <summary>
    /// Provides functionality to handle <see cref="DateTimeOffset"/> values for SQLite database operations.
    /// </summary>
    /// <remarks>This class is designed to parse and handle <see cref="DateTimeOffset"/> values stored as
    /// strings in SQLite. It extends <see cref="SqliteTypeHandler{T}"/> to provide type-specific parsing
    /// behavior.</remarks>
    public class DateTimeOffsetHandler : SqliteTypeHandler<DateTimeOffset>
    {
        /// <summary>
        /// Converts the specified object to a <see cref="DateTimeOffset"/> instance.
        /// </summary>
        /// <param name="value">The object to convert. Must be a string representing a valid <see cref="DateTimeOffset"/>.</param>
        /// <returns>A <see cref="DateTimeOffset"/> parsed from the specified string.</returns>
        public override DateTimeOffset Parse(object value)
            => DateTimeOffset.Parse((string)value);
    }
}
