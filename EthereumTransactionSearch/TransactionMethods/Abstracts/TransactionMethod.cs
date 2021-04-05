using EthereumTransactionSearch.Infura;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.InfuraMethods.Abstracts
{
    /// <summary>
    /// Abstract method with inT, outT, Execute and ExecuteAsync to be implemented.
    /// </summary>
    /// <typeparam name="inT"></typeparam>
    /// <typeparam name="outT"></typeparam>
    public abstract class TransactionMethod<inT, outT>
    {
        public abstract outT Execute(inT input);
        public abstract Task<outT> ExecuteAsync(inT input);
    }

}