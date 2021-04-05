using EthereumTransactionSearch.TransactionMethods;
using EthereumTransactionSearch.ValueObjects;
using Moq;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EthereumTransactionSearch.Test
{
    public class SpecsTests
    {
        [Fact]
        public void ResultsShouldMatchWithExpectedJson()
        {
            // arrange
            var mock = new Mock<GetListOfTransactionDetailsFromAddressInBlockMethod>();
            mock.CallBase = true;
            mock.Setup(x => x.GetBlockByNumber(new ValueObjects.BlockNumber(9148873), true)).Returns(Task.FromResult(File.ReadAllText("./TransactionMethodTests/GetListOfTransactionDetailsFromAddressInBlock/GetBlockByNumber_9148873_true_ResponsePayload.json")));

            var expectedJson = File.ReadAllText("./TransactionMethodTests/GetListOfTransactionDetailsFromAddressInBlock/GetBlockByNumber_9148873_true_ExpectedResults.json");
            var expectedObject = JsonConvert.DeserializeObject<TransactionDetails[]>(expectedJson);

            // act
            var y = mock.Object.Execute(((Address)"0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa", (BlockNumber)9148873)).ToArray();

            // assert            
            Assert.Equal(JsonConvert.SerializeObject(expectedObject), JsonConvert.SerializeObject(y));
        }

        [Fact]
        public void UnexisingAddressShouldReturnNoResult()
        {
            // arrange
            var mock = new Mock<GetListOfTransactionDetailsFromAddressInBlockMethod>();
            mock.CallBase = true;
            mock.Setup(x => x.GetBlockByNumber((BlockNumber)9148873, true)).Returns(Task.FromResult(File.ReadAllText("./TransactionMethodTests/GetListOfTransactionDetailsFromAddressInBlock/GetBlockByNumber_9148873_true_ResponsePayload.json")));

            // act
            var y = mock.Object.Execute(((Address)"0x_unexisting_address", (BlockNumber)9148873)).ToArray();

            // assert
            Assert.Equal("[]", JsonConvert.SerializeObject(y));
        }

        [Fact]
        public void UnexisingBlockShouldReturnNoResult()
        {
            // arrange
            var mock = new Mock<GetListOfTransactionDetailsFromAddressInBlockMethod>();
            mock.CallBase = true;
            mock.Setup(x => x.GetBlockByNumber((BlockNumber)0000001, true)).Returns(Task.FromResult(File.ReadAllText("./TransactionMethodTests/GetListOfTransactionDetailsFromAddressInBlock/GetBlockByNumber_0000000_true_ResponsePayload.json")));

            // act
            var y = mock.Object.Execute(((Address)"0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa", (BlockNumber)0000001)).ToArray();

            // assert
            Assert.Equal("[]", JsonConvert.SerializeObject(y));
        }
    }
}