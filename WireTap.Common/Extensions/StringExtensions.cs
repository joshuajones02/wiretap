namespace WireTap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static bool ContainsIgnoreCase(this IEnumerable<string> strings, string value) =>
            strings.Contains(value, StringComparer.InvariantCultureIgnoreCase);

        public static bool IsNullOrEmpty(this string value) =>
            string.IsNullOrEmpty(value);

        public static bool HasValue(this string value) =>
            !string.IsNullOrEmpty(value);

        public static bool EqualsIgnoreCase(this string @string, string value) =>
            @string != null
                ? @string.Equals(value, StringComparison.InvariantCultureIgnoreCase)
                : value == null;

        public static bool StartsWithIgnoreCase(this string @string, string value) =>
            @string.StartsWith(value, StringComparison.InvariantCultureIgnoreCase);

        public static string ToBase64(this string value) =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

        public static byte[] ToByteArray(this string value) =>
            Encoding.UTF8.GetBytes(value);

        public static string Mask(this string value, int exposed = 4)
        {
            if (value.IsNullOrEmpty())
                return value;
            if (value.Length <= exposed)
                return Regex.Replace(value, "[^- ]", "X");

            return string.Concat(
                Regex.Replace(value.Substring(0, value.Length - exposed), "[^- ]", "X"),
                value.Substring(value.Length - exposed));
        }
    }
}