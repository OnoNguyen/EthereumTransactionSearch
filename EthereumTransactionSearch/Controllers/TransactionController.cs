using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EthereumTransactionSearch.Infura;
using EthereumTransactionSearch.ValueObjects;
using EthereumTransactionSearch.InfuraMethods.MethodCollection;
using EthereumTransactionSearch.InfuraMethods;

namespace EthereumTransactionSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IInfuraMethodCollection _infuraMethodCollection;

        public TransactionController(IInfuraMethodCollection infuraMethodCollection)
        {
            _infuraMethodCollection = infuraMethodCollection;
        }
        [HttpGet("search")]
        public async Task<IEnumerable<TransactionDetails>> SearchAsync(string address, int blockNumber)
        {
            GetListOfTransactionDetailsFromAddressInBlockMethod methodInstance = _infuraMethodCollection.GetListOfTransactionDetailsFromAddressInBlockMethodInstance();
            var result = await methodInstance.ExecuteAsync((new Address(address), new BlockNumber(blockNumber)));

            return result.ToArray();
        }
    }
}