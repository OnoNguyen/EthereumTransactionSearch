using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EthereumTransactionSearch.Infura;
using EthereumTransactionSearch.InfuraMethods.Abstracts;
using EthereumTransactionSearch.ValueObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EthereumTransactionSearch.InfuraMethods
{

    public enum MethodNames
    {
        GetListOfTransactionDetailsFromAddressInBlockMethodFactory,
    }

    /// <summary>
    /// Concrete method
    /// </summary>
    public class GetListOfTransactionDetailsFromAddressInBlockMethod : InfuraMethod<(Address address, BlockNumber blockNumber), IEnumerable<TransactionDetails>>
    {
        public GetListOfTransactionDetailsFromAddressInBlockMethod()
        {
        }

        public virtual async Task<string> GetBlockByNumber(BlockNumber blockNumber,
            bool getTransactionDetails = false)
        {
            var blockNumberInHex = blockNumber.ToHex();
            var requestContentObject = new InfuraRequestContentV2(InfuraRequestMethods.GetBlockByNumber,
                new object[] { $"0x{blockNumberInHex}", getTransactionDetails });
            var requestContentJson = JsonConvert.SerializeObject(requestContentObject);
            var requestStringContent = new StringContent(requestContentJson, Encoding.UTF8,
                "application/json");

            var httpClient = new HttpClient();
            var getBlockByNumberResponse = await httpClient.PostAsync(InfuraApiEndpoint, requestStringContent);
            return await getBlockByNumberResponse.Content.ReadAsStringAsync();
        }

        public async Task<JArray> GetTransactionDetailsJArrayOfBlockNumber(BlockNumber blockNumber)
        {
            var getBlockNumberResponseContent = await GetBlockByNumber(blockNumber, true);
            var contentJObject = JObject.Parse(getBlockNumberResponseContent);
            if (string.IsNullOrWhiteSpace(contentJObject["result"].ToString()))
                return new JArray();

            var transactionsJObject = contentJObject["result"]["transactions"];
            return (JArray)transactionsJObject;
        }

        public async Task<IEnumerable<TransactionDetails>> GetListOfTransactionDetailsOfAddressInBlock((Address address, BlockNumber blockNumber) input)
        {
            var (address, blockNumber) = input;
            var transactionDetailsJArray = await GetTransactionDetailsJArrayOfBlockNumber(blockNumber);
            if (transactionDetailsJArray.Count == 0)
                return new List<TransactionDetails>().ToArray();

            var listOfTransactionDetails = transactionDetailsJArray
                .Where(token => token["from"].ToString() == address.ToString() || token["to"].ToString() == address.ToString())
                .Select(token => new TransactionDetails(
                    blockHash: token["blockHash"].ToString(),
                    blockNumberInHex: token["blockNumber"].ToString(),
                    token["gas"].ToString(),
                    token["hash"].ToString(),
                    token["from"].ToString(),
                    token["to"].ToString(),
                    token["value"].ToString()
                ));


            return listOfTransactionDetails;
        }

        public override IEnumerable<TransactionDetails> Execute((Address address, BlockNumber blockNumber) input)
            => GetListOfTransactionDetailsOfAddressInBlock(input).GetAwaiter().GetResult();

        public override async Task<IEnumerable<TransactionDetails>> ExecuteAsync((Address address, BlockNumber blockNumber) input)
            => await GetListOfTransactionDetailsOfAddressInBlock(input);
    }
}