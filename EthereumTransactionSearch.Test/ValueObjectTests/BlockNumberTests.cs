using EthereumTransactionSearch.Exceptions;
using EthereumTransactionSearch.ValueObjects;
using Xunit;

namespace EthereumTransactionSearch.Test
{
    public class BlockNumberTests
    {
        [Fact]
        public void BlockNumberLessThan1ShouldThrowBlockNumberOutOfRangeException()
        {
            // arrange & act
            var ex = Assert.Throws<BlockNumberOutOfRangeException>(() => new BlockNumber(0));

            // assert
            Assert.Equal("Block has to be greater than 0", ex.Message);
        }
    }
}