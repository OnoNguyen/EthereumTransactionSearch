using EthereumTransactionSearch.Factories;
using EthereumTransactionSearch.Factories.GetListOfTransactionDetailsFromAddressInBlock;
using EthereumTransactionSearch.Infura;
using Moq;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EthereumTransactionSearch.Test
{
    public class Tests
    {
        [Fact]
        public void ResultsShouldMatchWithExpectedJson()
        {
            // arrange
            var mock = new Mock<GetListOfTransactionDetailsFromAddressInBlockMethod>();
            mock.CallBase = true;
            mock.Setup(x => x.GetBlockByNumber(9148873, true)).Returns(Task.FromResult(File.ReadAllText("./MethodSpecsTests/GetListOfTransactionDetailsFromAddressInBlock/GetBlockByNumber_9148873_true_ResponsePayload.json")));
            mock.Setup(x => x.Input).Returns(("0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa", 9148873));

            // act
            var y = mock.Object.Execute().ToArray();

            // assert
            var expectedJson = File.ReadAllText("./MethodSpecsTests/GetListOfTransactionDetailsFromAddressInBlock/GetBlockByNumber_9148873_true_ExpectedResults.json");
            var expectedObject = JsonConvert.DeserializeObject<TransactionDetails[]>(expectedJson);
            
            Assert.Equal(JsonConvert.SerializeObject(expectedObject), JsonConvert.SerializeObject(y));
        }

        [Fact]
        public void UnexisingAddressShouldReturnNoResult()
        {
            // arrange
            var mock = new Mock<GetListOfTransactionDetailsFromAddressInBlockMethod>();
            mock.CallBase = true;
            mock.Setup(x => x.GetBlockByNumber(9148873, true)).Returns(Task.FromResult(File.ReadAllText("./MethodSpecsTests/GetListOfTransactionDetailsFromAddressInBlock/GetBlockByNumber_9148873_true_ResponsePayload.json")));
            mock.Setup(x => x.Input).Returns(("0x_unexisting_address", 9148873));

            // act
            var y = mock.Object.Execute().ToArray();

            // assert
            Assert.Equal("[]", JsonConvert.SerializeObject(y));

        }
        [Fact]
        public void UnexisingBlockShouldReturnNoResult()
        {
            // arrange
            var mock = new Mock<GetListOfTransactionDetailsFromAddressInBlockMethod>();
            mock.CallBase = true;
            mock.Setup(x => x.GetBlockByNumber(0000000, true)).Returns(Task.FromResult(File.ReadAllText("./MethodSpecsTests/GetListOfTransactionDetailsFromAddressInBlock/GetBlockByNumber_0000000_true_ResponsePayload.json")));
            mock.Setup(x => x.Input).Returns(("0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa", 0000000));

            // act
            var y = mock.Object.Execute().ToArray();

            // assert
            Assert.Equal("[]", JsonConvert.SerializeObject(y));

        }
    }
}