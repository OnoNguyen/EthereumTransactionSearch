using System;

namespace EthereumTransactionSearch.Exceptions
{
    public class AddressEmptyException : ArgumentException
    {
        public AddressEmptyException(string message) : base(message)
        {
        }
    }
}
