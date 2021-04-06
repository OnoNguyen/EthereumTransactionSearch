using System.Threading.Tasks;

namespace EthereumTransactionSearch.Infura.Abstracts
{
    public interface IInfuraMethod<inT>
    {
        Task<string> GetResponseStringAsync(inT input);
    }
}