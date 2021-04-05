using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.Infura.Abstracts
{
    public abstract class InfuraMethod<inT>
    {
        public abstract InfuraRequestContentV2 GetInfuraRequestContentV2(inT input);

        private static readonly string InfuraApiEndpoint = "https://mainnet.infura.io/v3/22b2ebe2940745b3835907b30e8257a4";

        public async Task<HttpResponseMessage> PostAsync(inT input)
        {
            var requestContentJson = JsonConvert.SerializeObject(GetInfuraRequestContentV2(input));
            var requestStringContent = new StringContent(requestContentJson, Encoding.UTF8,
                "application/json");

            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(InfuraApiEndpoint, requestStringContent);

            return response;
        }

        public async Task<string> GetResponseStringContentAsync(inT input)
        {
            var response = await PostAsync(input);
            return await response.Content.ReadAsStringAsync();
        }

    }
}