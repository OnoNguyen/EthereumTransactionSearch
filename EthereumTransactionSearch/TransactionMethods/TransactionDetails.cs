namespace EthereumTransactionSearch.TransactionMethods
{
    public class TransactionDetails
    {
        public TransactionDetails(string blockHash, string blockNumberInHex, string gas, string hash, string from,
            string to, string value)
        {
            BlockHash = blockHash;
            BlockNumberInHex = blockNumberInHex;
            Gas = gas;
            Hash = hash;
            From = from;
            To = to;
            Value = value;
        }

        public string BlockHash { get; private set; }
        public string BlockNumberInHex { get; private set; }
        public string Gas { get; private set; }
        public string Hash { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public string Value { get; private set; }
    }
}