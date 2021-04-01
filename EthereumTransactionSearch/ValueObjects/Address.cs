using EthereumTransactionSearch.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.ValueObjects
{
    public class Address: ValueObject<string>
    {
        public Address(string address): base(address, () => address.ThrowIfEmpty(nameof(address)))
        {
        }
    }
}
