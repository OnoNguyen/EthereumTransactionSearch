using EthereumTransactionSearch.Exceptions;

namespace EthereumTransactionSearch.ValueObjects
{
    public class Address: ValueObject<string>
    {
        public Address(string address): base(address, () => ThrowIfEmpty(address))
        {
        }

        public static explicit operator Address(string v)
        {
            return new Address(v);
        }

        public static void ThrowIfEmpty(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new AddressEmptyException("Address cannot be empty");
            }
        }
    }

}
