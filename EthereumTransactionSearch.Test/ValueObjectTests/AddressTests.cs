using EthereumTransactionSearch.Exceptions;
using EthereumTransactionSearch.ValueObjects;
using System;
using Xunit;

namespace EthereumTransactionSearch.Test
{
    public class AddressTests
    {
        [Fact]
        public void AddressEmptyShouldThrowAddressEmptyException()
        {
            // arrange
            // act
            var ex = Assert.Throws<AddressEmptyException>(() => new Address(string.Empty));

            // assert
            Assert.Equal("Address cannot be empty", ex.Message);
        }
    }
}