namespace IceCoffee.Db4Net.SqliteTypeHandlers
{
    /// <summary>
    /// Provides functionality to handle <see cref="Guid"/> values for SQLite database operations.
    /// </summary>
    /// <remarks>This class is designed to parse <see cref="Guid"/> values from SQLite database objects. It
    /// overrides the parsing behavior to convert a database value into a <see cref="Guid"/> instance.</remarks>
    public class GuidHandler : SqliteTypeHandler<Guid>
    {
        /// <summary>
        /// Converts the specified object to a <see cref="Guid"/> by parsing its string representation.
        /// </summary>
        /// <param name="value">The object containing the string representation of a <see cref="Guid"/>. Must be a non-null string.</param>
        /// <returns>A <see cref="Guid"/> parsed from the string representation provided in <paramref name="value"/>.</returns>
        public override Guid Parse(object value)
            => Guid.Parse((string)value);
    }
}
