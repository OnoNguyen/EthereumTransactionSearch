using EthereumTransactionSearch.ValueObjects;
using System;
using Xunit;

namespace EthereumTransactionSearch.Test
{
    public class BlockNumberTests
    {
        [Fact]
        public void BlockNumberLessThan1ShouldThrowException()
        {
            // arrange & act
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new BlockNumber(0));

            // assert
            Assert.Equal("Block has to be greater than 0", ex.Message);
        }
    }
}