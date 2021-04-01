using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EthereumTransactionSearch.Factories.Abstracts;
using EthereumTransactionSearch.Infura;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EthereumTransactionSearch.Factories.GetListOfTransactionDetailsFromAddressInBlock
{
    /// <summary>
    /// Concrete method
    /// </summary>
    public class GetListOfTransactionDetailsFromAddressInBlockMethod : InfuraMethod<(string address, int blockNumberInDec), IEnumerable<TransactionDetails>>
    {
        private readonly (string address, int blockNumberInDec) _input;

        /// <summary>
        /// for mocking
        /// </summary>
        [Obsolete("For mocking only")]
        public GetListOfTransactionDetailsFromAddressInBlockMethod()
        {             
        }

        public GetListOfTransactionDetailsFromAddressInBlockMethod((string address, int blockNumberInDec) input)
        {
            _input = input;
        }

        public override (string address, int blockNumberInDec) Input => _input;

        public override IEnumerable<TransactionDetails> Execute() => GetListOfTransactionDetailsOfAddressInBlock(Input).GetAwaiter().GetResult();
        public override async Task<IEnumerable<TransactionDetails>> ExecuteAsync() => await GetListOfTransactionDetailsOfAddressInBlock(Input);

        public virtual async Task<string> GetBlockByNumber(int blockNumberInDec,
            bool getTransactionDetails = false)
        {
            var blockNumberInHex = blockNumberInDec.ToString("X");
            var requestContentObject = new InfuraRequestContentV2(InfuraRequestMethods.GetBlockByNumber,
                new object[] { $"0x{blockNumberInHex}", getTransactionDetails });
            var requestContentJson = JsonConvert.SerializeObject(requestContentObject);
            var requestStringContent = new StringContent(requestContentJson, Encoding.UTF8,
                "application/json");

            var httpClient = new HttpClient();
            var getBlockByNumberResponse = await httpClient.PostAsync(InfuraApiEndpoint, requestStringContent);
            return await getBlockByNumberResponse.Content.ReadAsStringAsync();
        }

        public async Task<JArray> GetTransactionDetailsJArrayOfBlockNumber(int blockNumberInDec)
        {
            var getBlockNumberResponseContent = await GetBlockByNumber(blockNumberInDec, true);
            var contentJObject = JObject.Parse(getBlockNumberResponseContent);
            if (string.IsNullOrWhiteSpace(contentJObject["result"].ToString()))
                return new JArray();

            var transactionsJObject = contentJObject["result"]["transactions"];
            return (JArray)transactionsJObject;
        }

        public async Task<IEnumerable<TransactionDetails>> GetListOfTransactionDetailsOfAddressInBlock((string address, int blockNumberInDec) input)
        {
            var (address, blockNumberInDec) = input;
            var transactionDetailsJArray = await GetTransactionDetailsJArrayOfBlockNumber(blockNumberInDec);
            if (transactionDetailsJArray.Count == 0)
                return new List<TransactionDetails>().ToArray();

            var listOfTransactionDetails = transactionDetailsJArray
                .Where(token => token["from"].ToString() == address || token["to"].ToString() == address)
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
    }
}