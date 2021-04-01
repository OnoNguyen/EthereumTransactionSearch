using System;
using System.Collections.Generic;
using EthereumTransactionSearch.Factories.Abstracts;
using EthereumTransactionSearch.Infura;

namespace EthereumTransactionSearch.Factories.GetListOfTransactionDetailsFromAddressInBlock
{
    /// <summary>
    /// Concrete factory/creator
    /// </summary>
    public class GetListOfTransactionDetailsFromAddressInBlockMethodFactory : InfuraMethodFactory<(string, int), IEnumerable<TransactionDetails>>
    {
        public override InfuraMethod<(string, int), IEnumerable<TransactionDetails>> GetMethod((string, int) input)
            => new GetListOfTransactionDetailsFromAddressInBlockMethod(input);
    }
}