using System.Diagnostics.CodeAnalysis;

namespace IceCoffee.Db4Net.Core
{
    internal static class BraceExpansionHelper
    {
        public static bool TryExpand(string pattern, [NotNullWhen(true)] out List<string>? result)
        {
            result = null;

            try
            {
                int openBraceIndex = pattern.IndexOf('{');
                if (openBraceIndex == -1)
                {
                    return false;
                }

                int closeBraceIndex = FindMatchingBrace(pattern, openBraceIndex);
                if (closeBraceIndex == -1)
                {
                    return false;
                }

                string braceContent = pattern.Substring(openBraceIndex + 1, closeBraceIndex - openBraceIndex - 1);

                string[] options = braceContent.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                              .Select(option => option.Trim())
                                              .ToArray();

                string prefix = pattern.Substring(0, openBraceIndex);
                if (TryExpand(prefix, out List<string>? prefixExpansions) == false)
                {
                    prefixExpansions = new List<string> { prefix };
                }

                string suffix = pattern.Substring(closeBraceIndex + 1);
                if (TryExpand(suffix, out List<string>? suffixExpansions) == false)
                {
                    suffixExpansions = new List<string> { suffix };
                }

                result = new List<string>();
                foreach (string prefixExpansion in prefixExpansions)
                {
                    foreach (string option in options)
                    {
                        foreach (string suffixExpansion in suffixExpansions)
                        {
                            result.Add(prefixExpansion + option + suffixExpansion);
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static int FindMatchingBrace(string str, int openBraceIndex)
        {
            int braceCount = 1;
            for (int i = openBraceIndex + 1; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case '{':
                        braceCount++;
                        break;
                    case '}':
                        braceCount--;
                        if (braceCount == 0)
                            return i;
                        break;
                }
            }
            return -1;
        }
    }
}
