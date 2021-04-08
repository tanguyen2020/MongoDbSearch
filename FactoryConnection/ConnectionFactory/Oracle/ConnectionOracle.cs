using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace FactoryConnection.ConnectionFactory.Oracle
{
    public class ConnectionOracle : Execute
    {
        IConnection _oracle;
        public ConnectionOracle(IConnection oracle, string connectString): base(connectString)
        {
            _oracle = oracle;
        }
        public override DbDataAdapter CreateAdapter(DbCommand dbCommand) => new OracleDataAdapter(dbCommand as OracleCommand);
        public override DbConnection CreateConnection(string connectionString) => new OracleConnection(connectionString);
    }
}
