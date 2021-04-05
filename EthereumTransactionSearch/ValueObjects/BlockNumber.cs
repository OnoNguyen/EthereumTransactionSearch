using EthereumTransactionSearch.Exceptions;
using System;

namespace EthereumTransactionSearch.ValueObjects
{
    public class BlockNumber: ValueObject<int>
    {
        public BlockNumber(int block): base(block, () => ThrowIfNegative(block))
        {
        }

        private static void ThrowIfNegative(int v)
        {
            if (v <= 0)
                throw new BlockNumberOutOfRangeException("Block has to be greater than 0");
        }

        public string ToHex()
        {
            return this.Value.ToString("X");
            
        }

        public static explicit operator BlockNumber(int v)
        {
            return new BlockNumber(v);
        }
    }
}
