namespace FactoryConnection.ConnectionFactory
{
    public interface IConnection
    {
        IExecute CreateConnection();
    }
}
