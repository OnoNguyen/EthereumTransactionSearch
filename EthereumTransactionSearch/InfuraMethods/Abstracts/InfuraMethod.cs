using System.Threading.Tasks;

namespace EthereumTransactionSearch.InfuraMethods.Abstracts
{
    /// <summary>
    /// Abstract method with inT, outT, Execute and ExecuteAsync to be implemented.
    /// </summary>
    /// <typeparam name="inT"></typeparam>
    /// <typeparam name="outT"></typeparam>
    public abstract class InfuraMethod<inT, outT>
    {
        public abstract outT Execute(inT input);
        public abstract Task<outT> ExecuteAsync(inT input);

        /// <summary>
        /// can potentially read this from config
        /// </summary>
        protected static readonly string InfuraApiEndpoint =
            "https://mainnet.infura.io/v3/22b2ebe2940745b3835907b30e8257a4";
    }
}