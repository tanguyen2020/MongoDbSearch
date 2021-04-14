using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFConnection
{
    public static class DbOptionBuilderExtenssion
    {
        public static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder optionsBuilder, string connectString)
        {
            string[] prefix = connectString.Split(':');
            var prefixDbType = prefix[0].ToUpper();
            var prefixConnectString = prefix[1];
            switch (prefixDbType)
            {
                case prefixDatabaseType.MSSQL:
                    optionsBuilder.UseSqlServer(prefixConnectString); break;
                case prefixDatabaseType.ORACLE:
                    optionsBuilder.UseOracle(prefixConnectString); break;
                case prefixDatabaseType.MYSQL:
                    optionsBuilder.UseMySQL(prefixConnectString); break;
                default:
                    optionsBuilder.UseNpgsql(prefixConnectString); break;
            }
            return optionsBuilder;
        }
    }
}
