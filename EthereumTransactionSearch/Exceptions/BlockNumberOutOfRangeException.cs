using System;

namespace EthereumTransactionSearch.Exceptions
{
    public class BlockNumberOutOfRangeException : ArgumentOutOfRangeException
    {
        public BlockNumberOutOfRangeException(string message) : base("", message)
        {
        }
    }
}
