using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ADOConnection.ConnectionFactory.SqlServer;
using ADOConnection.ConnectionFactory.Oracle;
using ADOConnection.ConnectionFactory.PostgreSql;

namespace ADOConnection.ConnectionFactory
{
    public class Connection : IConnection
    {
        protected string ConnectionString;
        public IExecute CreateConnection()
        {
            try
            {
                IExecute execute;
                string[] prefix = this.ConnectionString.Split(':');
                var prefixdb = prefix[0].ToLower();
                var prefixConnectString = prefix[1];
                switch (prefixdb)
                {
                    case prefixDatabaseType.MSSQL:
                        execute = new ConnectionSql(this, prefixConnectString); break;
                    case prefixDatabaseType.ORACLE:
                        execute = new ConnectionOracle(this, prefixConnectString); break;
                    default:
                        execute = new ConnectionPostgre(this, prefixConnectString); break;
                }
                return execute;
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException("Prefix wrong configuration.");
            }
        }
    }
}
