﻿using EthereumTransactionSearch.Infura.Interfaces;
using EthereumTransactionSearch.ValueObjects;

namespace EthereumTransactionSearch.Infura
{
    public class GetBlockByNumber : InfuraMethod<(BlockNumber blockNumber, bool getTransactionDetails)>
    {
        public override InfuraRequestContentV2 GetInfuraRequestContentV2((BlockNumber blockNumber, bool getTransactionDetails) input)
            => new InfuraRequestContentV2(InfuraJsonRpcMethodNames.GetBlockByNumber,
                new object[] { $"0x{input.blockNumber.ToHex()}", input.getTransactionDetails });
    }
}