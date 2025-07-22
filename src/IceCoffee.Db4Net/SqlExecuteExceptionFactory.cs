using System.Diagnostics;

namespace IceCoffee.Db4Net
{
    internal class SqlExecuteExceptionFactory
    {
        public static SqlExecuteException Create(Exception inner, string sql, SqlCaptureMode mode)
        {
            if (ShouldIncludeSql(mode))
            {
                return new SqlExecuteException(inner, sql);
            }

            return new SqlExecuteException(inner);
        }

        private static bool ShouldIncludeSql(SqlCaptureMode mode)
        {
            return mode switch
            {
                SqlCaptureMode.OnlyDebuggerAttached => Debugger.IsAttached,
                SqlCaptureMode.Never => false,
                SqlCaptureMode.Always => true,
                _ => false
            };
        }
    }

}
