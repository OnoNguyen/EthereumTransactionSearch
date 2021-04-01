using System;

namespace EthereumTransactionSearch.ValueObjects
{
    public class Block: ValueObject<int>
    {
        public Block(int block): base(block, () => ThrowIfNegative(block))
        {
        }

        private static void ThrowIfNegative(int v)
        {
            if (v <= 0)
                throw new ArgumentOutOfRangeException(nameof(v), "Block has to be greater than 0");
        }
    }
}
