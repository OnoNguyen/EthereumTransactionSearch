using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using EthereumTransactionSearch.ValueObjects;
using EthereumTransactionSearch.Exceptions;
using EthereumTransactionSearch.TransactionMethods;
using System.Collections.Generic;

namespace EthereumTransactionSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private ITransactionMethod<(Address address, BlockNumber blockNumber), IEnumerable<TransactionDetails>> _getListOfTransactionDetailsFromAddressInBlockMethod;

        public TransactionController(ITransactionMethod<(Address address, BlockNumber blockNumber), IEnumerable<TransactionDetails>> getListOfTransactionDetailsFromAddressInBlockMethod)
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