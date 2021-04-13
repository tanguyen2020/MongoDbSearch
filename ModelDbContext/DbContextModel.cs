using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DatabaseContext;
using EFConnection;
using Microsoft.EntityFrameworkCore;

namespace ModelDbContext
{
    public class DbContextModel: BaseDbContext
    {
        private readonly ConnectionStringSettings _connectionStringSettings;
        public DbContextModel(DbContextOptions<DbContextModel> options, ConnectionStringSettings connectionStringSettings): base(options)
        {
            _connectionStringSettings = connectionStringSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseDatabase(_connectionStringSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
