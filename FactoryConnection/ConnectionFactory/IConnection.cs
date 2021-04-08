namespace ADOConnection.ConnectionFactory
{
    public interface IConnection
    {
        IExecute CreateConnection();
    }
}
