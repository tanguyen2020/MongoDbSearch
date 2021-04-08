using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;

namespace FactoryConnection.ConnectionFactory.SqlServer
{
    public class ConnectionSql : Execute
    {
        IConnection _sql;
        public ConnectionSql(IConnection sql, string connectString): base(connectString)
        {
            _sql = sql;
        }
        public override DbDataAdapter CreateAdapter(DbCommand dbCommand) => new SqlDataAdapter(dbCommand as SqlCommand);
        public override DbConnection CreateConnection(string connectionString) => new SqlConnection(connectionString);
    }
}
