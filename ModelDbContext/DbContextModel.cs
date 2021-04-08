using System;
using DatabaseContext;
using EFConnection;
using Microsoft.EntityFrameworkCore;

namespace ModelDbContext
{
    public class DbContextModel: BaseDbContext
    {
        private readonly ConnectionStringSettings _connectionStringSettings;
        public DbContextModel(ConnectionStringSettings connectionStringSettings)
        {
            _connectionStringSettings = connectionStringSettings;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseDatabase(_connectionStringSettings.ConnectionString);
        }
    }
}
