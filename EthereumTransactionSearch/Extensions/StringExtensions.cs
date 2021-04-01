using EthereumTransactionSearch.Exceptions;
using System;

namespace EthereumTransactionSearch.Extensions
{
    public static class StringExtensions
    {
        public static void ThrowIfEmpty(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new AddressEmptyException("Address cannot be empty");
            }
        }
    }
}
