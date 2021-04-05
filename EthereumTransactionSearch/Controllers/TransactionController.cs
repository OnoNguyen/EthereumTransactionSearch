using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using EthereumTransactionSearch.ValueObjects;
using EthereumTransactionSearch.Exceptions;
using System.Net;
using System;
using EthereumTransactionSearch.TransactionMethods;

namespace EthereumTransactionSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly GetListOfTransactionDetailsFromAddressInBlockMethod _getListOfTransactionDetailsFromAddressInBlockMethod;

        public TransactionController(GetListOfTransactionDetailsFromAddressInBlockMethod getListOfTransactionDetailsFromAddressInBlockMethod)
        {
            _getListOfTransactionDetailsFromAddressInBlockMethod = getListOfTransactionDetailsFromAddressInBlockMethod;
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchAsync(string address, int blockNumber)
        {
            try
            {
                var result = await _getListOfTransactionDetailsFromAddressInBlockMethod.ExecuteAsync((new Address(address), new BlockNumber(blockNumber)));

                return Ok(result.ToArray());
            }
            catch (BlockNumberOutOfRangeException ex1)
            {
                // TODO: inject logging
                return BadRequest(ex1);
            }
            catch (AddressEmptyException ex2)
            {
                // TODO: inject logging
                return BadRequest(ex2);
            }
        }
    }
}