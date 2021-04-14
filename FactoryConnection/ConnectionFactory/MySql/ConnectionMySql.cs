using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using MySql;
using MySql.Data.MySqlClient;

namespace ADOConnection.ConnectionFactory.MySql
{
    public class ConnectionMySql : Execute
    {
        private readonly IConnection _connection;
        public ConnectionMySql(IConnection connection, string connectString): base(connectString)
        {
            _connection = connection;
        }
        public override DbDataAdapter CreateAdapter(DbCommand dbCommand) => new MySqlDataAdapter(dbCommand as MySqlCommand);

        public override DbConnection CreateConnection(string connectionString) => new MySqlConnection(connectionString);
    }
}
