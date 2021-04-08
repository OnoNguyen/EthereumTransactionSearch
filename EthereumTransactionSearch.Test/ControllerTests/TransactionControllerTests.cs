using EthereumTransactionSearch.Controllers;
using EthereumTransactionSearch.Exceptions;
using EthereumTransactionSearch.TransactionMethods;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace EthereumTransactionSearch.Test
{
    public class TransactionControllerTests
    {
        [Fact]
        public async void SearchAsync_ShouldReturnOkIfNoException()
        {
            // arrange
            var mock = new Mock<GetListOfTransactionDetailsFromAddressInBlockMethod>();
            mock.CallBase = true;
            mock.Setup(x => x.GetBlockByNumber(new ValueObjects.BlockNumber(9148873), true)).Returns(Task.FromResult(File.ReadAllText("./TransactionMethodTests/GetListOfTransactionDetailsFromAddressInBlock/GetBlockByNumber_9148873_true_ResponsePayload.json")));

            var expectedJson = File.ReadAllText("./TransactionMethodTests/GetListOfTransactionDetailsFromAddressInBlock/GetBlockByNumber_9148873_true_ExpectedResults.json");
            var expectedObject = JsonConvert.DeserializeObject<TransactionDetails[]>(expectedJson);

            // act
            var transactionController = new TransactionController(mock.Object);
            var result = await transactionController.SearchAsync("abcdef", 9148873);

            // assert            
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void SearchAsync_ShouldReturnBadRequestIfAddressIsEmpty()
        {
            // arrange
            var mock = new Mock<GetListOfTransactionDetailsFromAddressInBlockMethod>();
            mock.CallBase = true;
            mock.Setup(x => x.GetBlockByNumber(new ValueObjects.BlockNumber(9148873), true)).Returns(Task.FromResult(File.ReadAllText("./TransactionMethodTests/GetListOfTransactionDetailsFromAddressInBlock/GetBlockByNumber_9148873_true_ResponsePayload.json")));

            var expectedJson = File.ReadAllText("./TransactionMethodTests/GetListOfTransactionDetailsFromAddressInBlock/GetBlockByNumber_9148873_true_ExpectedResults.json");
            var expectedObject = JsonConvert.DeserializeObject<TransactionDetails[]>(expectedJson);

            // act
            var transactionController = new TransactionController(mock.Object);
            var result = await transactionController.SearchAsync("", 9148873);

            // assert            
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<AddressEmptyException>(((ObjectResult)result).Value);
        }
        [Fact]
        public async void SearchAsync_ShouldReturnBadRequestIfBlockNumberIsLessThan1()
        {
            // arrange
            var mock = new Mock<GetListOfTransactionDetailsFromAddressInBlockMethod>();
            mock.CallBase = true;
            mock.Setup(x => x.GetBlockByNumber(new ValueObjects.BlockNumber(9148873), true)).Returns(Task.FromResult(File.ReadAllText("./TransactionMethodTests/GetListOfTransactionDetailsFromAddressInBlock/GetBlockByNumber_9148873_true_ResponsePayload.json")));

            var expectedJson = File.ReadAllText("./TransactionMethodTests/GetListOfTransactionDetailsFromAddressInBlock/GetBlockByNumber_9148873_true_ExpectedResults.json");
            var expectedObject = JsonConvert.DeserializeObject<TransactionDetails[]>(expectedJson);

            // act
            var transactionController = new TransactionController(mock.Object);
            var result = await transactionController.SearchAsync("abcd", -1);

            // assert            
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<BlockNumberOutOfRangeException>(((ObjectResult)result).Value);
        }
    }
}