using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EthereumTransactionSearch.InfuraMethods.Abstracts;
using EthereumTransactionSearch.TransactionMethods.Abstracts;
using EthereumTransactionSearch.ValueObjects;
using Newtonsoft.Json.Linq;

namespace EthereumTransactionSearch.TransactionMethods
{
    /// <summary>
    /// Concrete method
    /// </summary>
    public class GetListOfTransactionDetailsFromAddressInBlockMethod : ITransactionMethod<(Address address, BlockNumber blockNumber), IEnumerable<TransactionDetails>>
    {
        private IInfuraMethod<(BlockNumber, bool)> _getBlockByNumber;

        public GetListOfTransactionDetailsFromAddressInBlockMethod()
        {

        }

        public GetListOfTransactionDetailsFromAddressInBlockMethod(IInfuraMethod<(BlockNumber, bool)> getBlockByNumber)
        {
            _getBlockByNumber = getBlockByNumber;
        }

        public virtual async Task<string> GetBlockByNumber(BlockNumber blockNumber, bool getTransactionDetails = false)
        {
            return await _getBlockByNumber.GetResponseStringAsync((blockNumber, getTransactionDetails));
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

        public IEnumerable<TransactionDetails> Execute((Address address, BlockNumber blockNumber) input)
            => GetListOfTransactionDetails(input).GetAwaiter().GetResult();

        public async Task<IEnumerable<TransactionDetails>> ExecuteAsync((Address address, BlockNumber blockNumber) input)
            => await GetListOfTransactionDetails(input);
    }
}