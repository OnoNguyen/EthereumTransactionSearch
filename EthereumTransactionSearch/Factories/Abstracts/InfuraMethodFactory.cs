namespace EthereumTransactionSearch.Factories.Abstracts
{
    /// <summary>
    /// MethodFactory (creator)
    /// </summary>
    /// <typeparam name="inT"></typeparam>
    /// <typeparam name="outT"></typeparam>
    public abstract class InfuraMethodFactory<inT, outT>
    {
        public abstract InfuraMethod<inT, outT> GetMethod(inT input);
    }
}