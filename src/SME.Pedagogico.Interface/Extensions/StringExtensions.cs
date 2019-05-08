using System;

namespace SME.Pedagogico.Interface.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotNull(this string value)
        {
            return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNull(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        public static bool IsEqualsTo(this string source, string target)
        {
            return string.Equals(source,
                                target,
                                StringComparison.InvariantCultureIgnoreCase
                                );
        }
    }
}
