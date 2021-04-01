using System;
using System.Collections.Generic;
using EthereumTransactionSearch.Controllers;
using EthereumTransactionSearch.Factories.Abstracts;

namespace EthereumTransactionSearch.Factories
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