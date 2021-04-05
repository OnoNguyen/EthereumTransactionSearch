using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace EthereumTransactionSearch.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfuraMethods(this IServiceCollection services, List<Type> infuraMethods)
        {
            foreach (Type method in infuraMethods)
            {
                services.AddSingleton(method);
            }
        }
        public static void AddTransactionMethods(this IServiceCollection services, List<Type> transactionMethods)
        {
            foreach (Type method in transactionMethods)
            {
                services.AddSingleton(method);
            }
        }
    }
}
