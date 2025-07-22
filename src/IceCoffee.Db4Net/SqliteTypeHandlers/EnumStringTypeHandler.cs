//using Dapper;
//using System.Data;

//namespace IceCoffee.Db4Net.SqliteTypeHandlers
//{
//    /// <summary>
//    /// A generic Dapper type handler for converting Enums to and from strings 
//    /// for storage in a database TEXT column.
//    /// </summary>
//    /// <typeparam name="T">The Enum type to handle.</typeparam>
//    public class EnumStringTypeHandler<T> : SqlMapper.TypeHandler<T> where T : Enum
//    {
//        /// <summary>
//        /// Parses a string value from the database back to its Enum representation.
//        /// </summary>
//        /// <param name="value">The object from the database (expected to be a string).</param>
//        /// <returns>The parsed Enum value.</returns>
//        public override T Parse(object value)
//        {
//            if (value == null || value is DBNull)
//            {
//                // Return the default value for the enum (usually the member with value 0)
//                // or throw an exception, depending on your business logic.
//                return default!;
//            }

//            try
//            {
//                // Parse the string to the corresponding enum value, ignoring case.
//                return (T)Enum.Parse(typeof(T), value.ToString()!, true);
//            }
//            catch (Exception ex)
//            {
//                throw new InvalidCastException($"Cannot convert '{value}' to enum type '{typeof(T).Name}'.", ex);
//            }
//        }

//        /// <summary>
//        /// Sets the value of a database parameter from an Enum value.
//        /// </summary>
//        /// <param name="parameter">The database command parameter.</param>
//        /// <param name="value">The Enum value to be stored.</param>
//        public override void SetValue(IDbDataParameter parameter, T value)
//        {
//            parameter.DbType = DbType.String;
//            parameter.Value = value.ToString();
//        }
//    }
//}
