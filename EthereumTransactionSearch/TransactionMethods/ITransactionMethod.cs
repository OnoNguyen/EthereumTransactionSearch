using System.Threading.Tasks;

namespace EthereumTransactionSearch.TransactionMethods
{
    /// <summary>
    /// Abstract method with inT, outT, Execute and ExecuteAsync to be implemented.
    /// </summary>
    /// <typeparam name="inT"></typeparam>
    /// <typeparam name="outT"></typeparam>
    public interface ITransactionMethod<inT, outT>
    {
        outT Execute(inT input);
        Task<outT> ExecuteAsync(inT input);
    }

}