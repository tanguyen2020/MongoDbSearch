using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Npgsql;

namespace FactoryConnection.ConnectionFactory.PostgreSql
{
    public class ConnectionPostgre : Execute
    {
        IConnection _postgre;
        public ConnectionPostgre(IConnection postgre, string connectString): base (connectString)
        {
            _postgre = postgre;
        }
        public override DbDataAdapter CreateAdapter(DbCommand dbCommand) => new NpgsqlDataAdapter(dbCommand as NpgsqlCommand);
        public override DbConnection CreateConnection(string connectionString) => new NpgsqlConnection(connectionString);
    }
}
