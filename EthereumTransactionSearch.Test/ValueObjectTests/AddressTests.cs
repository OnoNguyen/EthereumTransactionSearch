using EthereumTransactionSearch.ValueObjects;
using System;
using Xunit;

namespace EthereumTransactionSearch.Test
{
    public class AddressTests
    {
        [Fact]
        public void AddressEmptyShouldThrowException()
        {
            // arrange
            // act
            var ex = Assert.Throws<ArgumentException>(() => new Address(string.Empty));

            // assert
            Assert.Equal("Address cannot be empty", ex.Message);
        }
    }
}