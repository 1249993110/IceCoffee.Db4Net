using Dapper;

namespace IceCoffee.Db4Net.SqliteTypeHandlers
{
    /// <summary>
    /// See https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/dapper-limitations#data-types
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SqliteTypeHandler<T> : SqlMapper.TypeHandler<T>
    {
        /// <summary>
        /// Sets the value of the specified database parameter.
        /// </summary>
        /// <remarks>This method assigns the provided value to the <see
        /// cref="System.Data.IDbDataParameter"/> property of the specified parameter. Ensure that the parameter
        /// is properly configured to accept the type of the value being assigned.</remarks>
        /// <param name="parameter">The database parameter whose value is to be set. Cannot be null.</param>
        /// <param name="value">The value to assign to the parameter. Can be null if the parameter supports null values.</param>
        public override void SetValue(System.Data.IDbDataParameter parameter, T? value)
            => parameter.Value = value;
    }
}