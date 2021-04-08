﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using FactoryConnection.ConnectionFactory.SqlServer;
using FactoryConnection.ConnectionFactory.Oracle;
using FactoryConnection.ConnectionFactory.PostgreSql;

namespace FactoryConnection.ConnectionFactory
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
                    case prefixConnection.MSSQL:
                        execute = new ConnectionSql(this, prefixConnectString); break;
                    case prefixConnection.ORACLE:
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
