using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.Infura
{
    public static class InfuraHttpClient
    {
        private static readonly string InfuraApiEndpoint =
            "https://mainnet.infura.io/v3/22b2ebe2940745b3835907b30e8257a4";

        public static async Task<HttpResponseMessage> PostAsync(InfuraRequestContentV2 requestContentObject)
        {
            var requestContentJson = JsonConvert.SerializeObject(requestContentObject);
            var requestStringContent = new StringContent(requestContentJson, Encoding.UTF8,
                "application/json");

            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(InfuraApiEndpoint, requestStringContent);

            return response;
        }
    }
}
