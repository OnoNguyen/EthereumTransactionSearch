using Newtonsoft.Json;

namespace EthereumTransactionSearch.Infura
{
    public class InfuraRequestContentV2
    {
        [JsonProperty("jsonrpc")] public readonly string JsonRpc = "2.0";
        [JsonProperty("id")] public readonly int Id = 1;
        [JsonProperty("method")] public readonly string Method;
        [JsonProperty("params")] public readonly object[] Params;

        public InfuraRequestContentV2(string method, object[] @params)
        {
            Method = method;
            Params = @params;
        }
    }
}