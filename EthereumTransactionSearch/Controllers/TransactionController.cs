﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EthereumTransactionSearch.Factories.GetListOfTransactionDetailsFromAddressInBlock;
using EthereumTransactionSearch.Infura;

namespace EthereumTransactionSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        [HttpGet("search")]
        public async Task<IEnumerable<TransactionDetails>> SearchAsync(string address, int blockNumber)
        {
            var methodFactory = new GetListOfTransactionDetailsFromAddressInBlockMethodFactory();
            var methodInstance = methodFactory.GetMethod((address, blockNumber));
            var result = await methodInstance.ExecuteAsync();

            return result.ToArray();
        }
    }
}