using Dapper;
using IceCoffee.Db4Net.Core;

namespace IceCoffee.Db4Net.Extensions
{
    internal static class SqlResultExtension
    {
        public static object? GetParameters(this SqlResult sqlResult)
        {
            if (sqlResult.Entities != null)
            {
                return sqlResult.Entities;
            }

            DynamicParameters? parameters = null;
            if (sqlResult.NamedParameters != null)
            {
                parameters ??= new DynamicParameters();
                foreach (var param in sqlResult.NamedParameters)
                {
                    parameters.Add(param.Key, param.Value);
                }
            }

            if (sqlResult.DynamicParameters != null)
            {
                parameters ??= new DynamicParameters();
                foreach (var param in sqlResult.DynamicParameters)
                {
                    parameters.AddDynamicParams(param);
                }
            }

            return parameters;
        }
    }
}
