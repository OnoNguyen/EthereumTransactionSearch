using EthereumTransactionSearch.InfuraMethods.Abstracts;
using EthereumTransactionSearch.ValueObjects;

namespace EthereumTransactionSearch.InfuraMethods
{
    public class GetBlockByNumber : InfuraMethod<(BlockNumber blockNumber, bool getTransactionDetails)>
    {
        protected override InfuraRequestContentV2 GetInfuraRequestContentV2((BlockNumber blockNumber, bool getTransactionDetails) input)
            => new InfuraRequestContentV2("eth_getBlockByNumber",
                new object[] { $"0x{input.blockNumber.ToHex()}", input.getTransactionDetails });
    }
}