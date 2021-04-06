using EthereumTransactionSearch.Infura.Abstracts;
using EthereumTransactionSearch.ValueObjects;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.Infura
{
    public class GetBlockByNumber : InfuraMethod<(BlockNumber blockNumber, bool getTransactionDetails)>
    {
        protected override InfuraRequestContentV2 GetInfuraRequestContentV2((BlockNumber blockNumber, bool getTransactionDetails) input)
            => new InfuraRequestContentV2(InfuraJsonRpcMethodNames.GetBlockByNumber,
                new object[] { $"0x{input.blockNumber.ToHex()}", input.getTransactionDetails });
    }
}