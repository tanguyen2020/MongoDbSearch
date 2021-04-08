using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using BaseObject.DataObject;

namespace EFConnection
{
    public class BaseDbContext: DbContext
    {
        public virtual DbSet<T> Repository<T>() where T: class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task<TDocument> QueryForObjectAsync<TDocument>(string sql, IDictionary<string, object> param = null) where TDocument : class
        {
            return await base.Set<TDocument>().FromSqlRaw(sql, ParseParameters(param)).FirstOrDefaultAsync();
        }

        public async Task<List<TDocument>> QueryForListAsync<TDocument>(string sql, IDictionary<string, object> param = null) where TDocument : class
        {
            return await base.Set<TDocument>().FromSqlRaw(sql, ParseParameters(param)).ToListAsync();
        }

        public Dictionary<string, object> ParseParameters(IDictionary<string, object> param)
        {
            var parameters = new Dictionary<string, object>();
            foreach(var i in param)
            {
                parameters.Add($"@{i.Key}", i.Value ?? DBNull.Value);
            }
            return parameters;
        }
    }
}
