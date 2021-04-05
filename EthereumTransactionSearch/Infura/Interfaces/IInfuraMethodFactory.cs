namespace EthereumTransactionSearch.Infura.Interfaces
{
    public class InfuraMethodFactory<inT, mT> where mT : InfuraMethod<inT>, new()
    {
        public InfuraMethod<inT> MakeInstance()
        {
            return new mT();
        }
    }
}
