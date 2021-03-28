
namespace EthereumTransactionSearch.Factories
{
    public class InfuraHttpClientInstanceFactory
    {
        public static readonly string InfuraApiEndpoint =
            "https://mainnet.infura.io/v3/22b2ebe2940745b3835907b30e8257a4";
        public InfuraHttpClientInstance GetInstance()
        {
            return new InfuraHttpClientInstance(InfuraApiEndpoint);
        }
    }
}
