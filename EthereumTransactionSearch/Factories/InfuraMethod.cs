using System.Threading.Tasks;

namespace EthereumTransactionSearch.Factories
{
    /// <summary>
    /// Abstract method with inT, outT, Execute and ExecuteAsync to be implemented.
    /// </summary>
    /// <typeparam name="inT"></typeparam>
    /// <typeparam name="outT"></typeparam>
    public abstract class InfuraMethod<inT, outT>
    {
        public abstract inT Input { get; }
        public abstract outT Execute();
        public abstract Task<outT> ExecuteAsync();

        protected static readonly string InfuraApiEndpoint =
            "https://mainnet.infura.io/v3/22b2ebe2940745b3835907b30e8257a4";
    }
}