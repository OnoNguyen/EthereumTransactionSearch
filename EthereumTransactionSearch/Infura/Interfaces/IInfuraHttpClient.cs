using System.Net.Http;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.Infura.Interfaces
{
    public interface IInfuraHttpClient
    {
        Task<HttpResponseMessage> PostAsync(InfuraRequestContentV2 requestContentObject);
    }
}