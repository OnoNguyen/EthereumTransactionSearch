using System.Linq;
using EthereumTransactionSearch.Factories;
using Xunit;

namespace EthereumTransactionSearch.Test
{
    public class InfuraIntegrationTests
    {
        [Fact]
        public async void GetBlockByNumberShouldReturnNoErrors()
        {
            // arrange
            var infuraClient = new InfuraHttpClientInstanceFactory().GetInstance();

            // act
            var getBlockByNumberResponse = await infuraClient.GetBlockByNumber(9148873);

            // assert
            var result = getBlockByNumberResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Assert.DoesNotContain("error", result);
        }

        [Fact]
        public async void GetTransactionDetailsJObjectOfBlockByNumberShouldReturnAValidJArray()
        {
            // arrange
            var infuraClient = new InfuraHttpClientInstanceFactory().GetInstance();

            // act
            var transactionDetailsJArray = await infuraClient.GetTransactionDetailsJArrayOfBlockNumber(9148873);

            // assert
            Assert.True(transactionDetailsJArray.Count > 0);
        }

        [Fact]
        public async void GetTransactionDetailsJObjectOfBlockByNumberShouldReturnCorrectCount()
        {
            // arrange
            var infuraClient = new InfuraHttpClientInstanceFactory().GetInstance();

            // act
            var listOfTransactionDetailsOfAddressInBlock =
                await infuraClient.GetListOfTransactionDetailsOfAddressInBlock(
                    "0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa", 9148873);

            // assert
            Assert.Equal(2, listOfTransactionDetailsOfAddressInBlock.Count());
        }
    }
}