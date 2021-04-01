using System;

namespace EthereumTransactionSearch.Extensions
{
    public static class StringExtensions
    {
        public static void ThrowIfEmpty(this string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Address cannot be empty");
            }
        }
    }
}
