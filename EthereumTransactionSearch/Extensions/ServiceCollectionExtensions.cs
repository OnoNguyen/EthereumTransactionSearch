using EthereumTransactionSearch.InfuraMethods;
using EthereumTransactionSearch.InfuraMethods.Abstracts;
using EthereumTransactionSearch.TransactionMethods;
using EthereumTransactionSearch.TransactionMethods.Abstracts;
using EthereumTransactionSearch.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace EthereumTransactionSearch.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfuraMethods(this IServiceCollection services)
        {
            services.AddSingleton<IInfuraMethod<(BlockNumber, bool)>, GetBlockByNumber>();
        }
        public static void AddTransactionMethods(this IServiceCollection services)
        {
            services.AddSingleton<ITransactionMethod<(Address address, BlockNumber blockNumber), IEnumerable<TransactionDetails>>, GetListOfTransactionDetailsFromAddressInBlockMethod>();
        }
    }
}
