using EthereumTransactionSearch.ValueObjects;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.Infura
{
    public static class InfuraRequestMethods
    {
        public static async Task<string> GetBlockByNumber(BlockNumber blockNumber,
            bool getTransactionDetails = false)
        {
            var requestContentObject = new InfuraRequestContentV2(InfuraRequestMethodNames.GetBlockByNumber,
                new object[] { $"0x{blockNumber.ToHex()}", getTransactionDetails });

            var getBlockByNumberResponse = await InfuraHttpClient.PostAsync(requestContentObject);
            return await getBlockByNumberResponse.Content.ReadAsStringAsync();
        }
    }
}