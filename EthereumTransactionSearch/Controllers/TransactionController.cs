using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using EthereumTransactionSearch.ValueObjects;
using EthereumTransactionSearch.InfuraMethods.MethodCollection;
using EthereumTransactionSearch.InfuraMethods;
using EthereumTransactionSearch.Exceptions;
using System.Net;
using System;

namespace EthereumTransactionSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        public TransactionController()
        {
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchAsync(string address, int blockNumber)
        {
            try
            {
                GetListOfTransactionDetailsFromAddressInBlockMethod methodInstance = TransactionMethods.GetListOfTransactionDetailsFromAddressInBlockMethodInstance();
                var result = await methodInstance.ExecuteAsync((new Address(address), new BlockNumber(blockNumber)));

                return Ok(result.ToArray());
            }
            catch (BlockNumberOutOfRangeException ex1)
            {
                // TODO: inject logging
                return StatusCode((int)HttpStatusCode.BadRequest, ex1.Message);
            }
            catch (AddressEmptyException ex2)
            {
                // TODO: inject logging
                return StatusCode((int)HttpStatusCode.BadRequest, ex2.Message);
            }
            catch (Exception ex3)
            {                
                // TODO: inject logging
                return StatusCode((int)HttpStatusCode.InternalServerError, ex3.Message);
            }
        }
    }
}