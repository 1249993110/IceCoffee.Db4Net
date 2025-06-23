using System.Text.RegularExpressions;

namespace IceCoffee.Db4Net.Core
{
    internal static partial class Utils
    {
#if NETCOREAPP
        [GeneratedRegex(@"^[A-Za-z_][A-Za-z0-9_]*$")]
        private static partial Regex Regex();
        public static bool IsValidSqlIdentifier(string name)
        {
            return Regex().IsMatch(name);
        }
#else
        public static bool IsValidSqlIdentifier(string name)
        {
            //return name.Length > 0 && name.IndexOfAny(_invalidStartChars) != 0 && Regex.IsMatch(name, @"^[A-Za-z_][A-Za-z0-9_]*$");
            if (string.IsNullOrEmpty(name))
                return false;

            char firstChar = name[0];
            if (!((firstChar >= 'A' && firstChar <= 'Z') || (firstChar >= 'a' && firstChar <= 'z') || firstChar == '_'))
                return false;

            for (int i = 1, len = name.Length; i < len; i++)
            {
                char c = name[i];
                if (!((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || c == '_'))
                    return false;
            }

            return true;
        }
#endif

        public static string ParseFormattableString(FormattableString formattableString, Func<object?, string> addParam)
        {
            int parameterCount = formattableString.ArgumentCount;

            if (parameterCount == 0)
            {
                return formattableString.Format;
            }

            string[] parameterNames = new string[parameterCount];
            for (int i = 0; i < parameterCount; i++)
            {
                parameterNames[i] = addParam.Invoke(formattableString.GetArgument(i));
            }

            return string.Format(formattableString.Format, parameterNames);
        }
    }
}
