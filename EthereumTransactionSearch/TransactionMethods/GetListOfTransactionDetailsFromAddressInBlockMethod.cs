using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EthereumTransactionSearch.Infura;
using EthereumTransactionSearch.InfuraMethods.Abstracts;
using EthereumTransactionSearch.ValueObjects;
using Newtonsoft.Json.Linq;

namespace EthereumTransactionSearch.InfuraMethods
{
    /// <summary>
    /// Concrete method
    /// </summary>
    public class GetListOfTransactionDetailsFromAddressInBlockMethod : TransactionMethod<(Address address, BlockNumber blockNumber), IEnumerable<TransactionDetails>>
    {
        public GetListOfTransactionDetailsFromAddressInBlockMethod()
        {
        }

        public virtual async Task<string> GetBlockByNumber(BlockNumber blockNumber,
            bool getTransactionDetails = false)
        {
            var requestContentObject = new InfuraRequestContentV2(InfuraRequestMethodNames.GetBlockByNumber,
                new object[] { $"0x{blockNumber.ToHex()}", getTransactionDetails });

            var getBlockByNumberResponse = await InfuraHttpClient.PostAsync(requestContentObject);
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

        public async Task<IEnumerable<TransactionDetails>> GetListOfTransactionDetails((Address address, BlockNumber blockNumber) input)
        {
            var (address, blockNumber) = input;
            var transactionDetailsJArray = await GetTransactionDetailsJArrayOfBlockNumber(blockNumber);
            
            if (transactionDetailsJArray.Count == 0)
                return new List<TransactionDetails>().ToArray();

            var listOfTransactionDetails = transactionDetailsJArray
                .Where(token => token["from"].ToString() == address.ToString() || token["to"].ToString() == address.ToString())
                .Select(token => MapTokenToTransactionDetails(token));


            return listOfTransactionDetails;
        }

        private TransactionDetails MapTokenToTransactionDetails(JToken token)
            => new TransactionDetails(
                    blockHash: token["blockHash"].ToString(),
                    blockNumberInHex: token["blockNumber"].ToString(),
                    gas: token["gas"].ToString(),
                    hash: token["hash"].ToString(),
                    from: token["from"].ToString(),
                    to: token["to"].ToString(),
                    value: token["value"].ToString()
                );

        public override IEnumerable<TransactionDetails> Execute((Address address, BlockNumber blockNumber) input)
            => GetListOfTransactionDetails(input).GetAwaiter().GetResult();

        public override async Task<IEnumerable<TransactionDetails>> ExecuteAsync((Address address, BlockNumber blockNumber) input)
            => await GetListOfTransactionDetails(input);
    }
}