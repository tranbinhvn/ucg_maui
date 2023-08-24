using System.Globalization;
using System.Text.RegularExpressions;

namespace UCG.siteTRAXLite.Common.Extensions
{
    public static class StringExtensions
    {
        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }

        public static bool EqualsWithCulture(this string s, string parameter)
        {
            return s.Equals(parameter);
        }

        public static string ToStringWithCulture(this object s)
        {
            return s.ToString();
        }

        public static string ToLowerWithCulture(this string s)
        {
            return s.ToLower();
        }

        public static string ToUpperWithCulture(this string s)
        {
            return s.ToUpper();
        }

        public static bool StartsWithWithCulture(this string s, string data)
        {
            return s.StartsWith(data);
        }

        public static string ODataEncode(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = s.Replace("'", "''")
                     .Replace("+", "%2B")
                     .Replace("/", "%2F")
                     .Replace("?", "%3F")
                     .Replace("%", "%25")
                     .Replace("#", "%23")
                     .Replace("&", "%26");
            }
            return s;
        }

        public static string ToTitleCaseWithCulture(this string s)
        {
            s?.Trim();
            if (!string.IsNullOrEmpty(s))
            {
                var textInfo = CultureInfo.CurrentCulture.TextInfo;
                return textInfo.ToTitleCase(s.ToLower());
            }
            return string.Empty;
        }
    }
}
