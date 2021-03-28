using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EthereumTransactionSearch.Controllers;
using EthereumTransactionSearch.Infura;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EthereumTransactionSearch.Factories
{
    public class InfuraHttpClientInstance : HttpClient
    {
        public InfuraHttpClientInstance(string infuraApiEndpoint)
        {
            BaseAddress = new Uri(infuraApiEndpoint);
        }

        public async Task<HttpResponseMessage> GetBlockByNumber(int blockNumberInDec,
            bool getTransactionDetails = false)
        {
            var blockNumberInHex = blockNumberInDec.ToString("X");
            var requestContentObject = new InfuraRequestContentV2(InfuraRequestMethods.GetBlockByNumber,
                new object[] {$"0x{blockNumberInHex}", getTransactionDetails});
            var requestContentJson = JsonConvert.SerializeObject(requestContentObject);
            var requestStringContent = new StringContent(requestContentJson, Encoding.UTF8,
                "application/json");

            var getBlockByNumberResponse = await PostAsync(BaseAddress, requestStringContent);
            return getBlockByNumberResponse;
        }

        public async Task<JArray> GetTransactionDetailsJArrayOfBlockNumber(int blockNumberInDec)
        {
            var getBlockByNumberResponse = await GetBlockByNumber(blockNumberInDec, true);
            var getBlockNumberResponseContent = await getBlockByNumberResponse.Content.ReadAsStringAsync();
            var contentJObject = JObject.Parse(getBlockNumberResponseContent);
            var transactionsJObject = contentJObject["result"]["transactions"];
            return (JArray) transactionsJObject;
        }

        public async Task<IEnumerable<TransactionDetails>> GetListOfTransactionDetailsOfAddressInBlock(string address,
            int blockNumberInDec)
        {
            var transactionDetailsJArray = await GetTransactionDetailsJArrayOfBlockNumber(blockNumberInDec);
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