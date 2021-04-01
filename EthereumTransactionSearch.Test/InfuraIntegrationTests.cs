using System.Linq;
using EthereumTransactionSearch.Factories;
using EthereumTransactionSearch.Factories.GetListOfTransactionDetailsFromAddressInBlock;
using Newtonsoft.Json;
using Xunit;

namespace EthereumTransactionSearch.Test
{
    public class InfuraIntegrationTests
    {
        [Fact]
        public async void GetBlockByNumberShouldReturnNoErrors()
        {
            // arrange
            var methodFactory = new GetListOfTransactionDetailsFromAddressInBlockMethodFactory();
            var methodInstance = new GetListOfTransactionDetailsFromAddressInBlockMethod(("0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa", 000033333));

            // act
            var getBlockByNumberResponse = await methodInstance.GetBlockByNumber(00221515, true);

            // assert
            var result = getBlockByNumberResponse;
            Assert.DoesNotContain("error", result);
        }

        [Fact]
        public async void GetTransactionDetailsJObjectOfBlockByNumberShouldReturnAValidJArray()
        {
            // arrange
            var methodFactory = new GetListOfTransactionDetailsFromAddressInBlockMethodFactory();
            var methodInstance = new GetListOfTransactionDetailsFromAddressInBlockMethod(("0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa", 9148873));

            // act
            var transactionDetailsJArray = await methodInstance.GetTransactionDetailsJArrayOfBlockNumber(9148873);

            // assert
            Assert.True(transactionDetailsJArray.Count > 0);
        }

        [Fact]
        public async void GetTransactionDetailsJObjectOfBlockByNumberShouldReturnCorrectCount()
        {
            // arrange
            var methodFactory = new GetListOfTransactionDetailsFromAddressInBlockMethodFactory();
            var methodInstance = new GetListOfTransactionDetailsFromAddressInBlockMethod(("0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa", 9148873));
            // act
            var listOfTransactionDetailsOfAddressInBlock =
                await methodInstance.GetListOfTransactionDetailsOfAddressInBlock(
                    ("0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa", 9148873));

            JsonConvert.SerializeObject(listOfTransactionDetailsOfAddressInBlock.ToArray());
            // assert
            Assert.Equal(2, listOfTransactionDetailsOfAddressInBlock.Count());
        }
    }
}