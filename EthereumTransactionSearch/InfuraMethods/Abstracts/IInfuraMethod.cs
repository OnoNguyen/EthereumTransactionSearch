using System.Threading.Tasks;

namespace EthereumTransactionSearch.InfuraMethods.Abstracts
{
    public interface IInfuraMethod<inT>
    {
        Task<string> GetResponseStringAsync(inT input);
    }
}