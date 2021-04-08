using BaseObject.DataObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFConnection
{
    public class GenericContext<T> : IGenericContext<T> where T: BaseDbContext, IDisposable
    {
        protected bool disposed = false;
        public readonly T _dbContext;
        public DbSet<TEntity> Repository<TEntity>() where TEntity : class
        {
            return _dbContext.Repository<TEntity>();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public TDocument QueryForObject<TDocument>(string sql, IDictionary<string, object> param = null) where TDocument : class
        {
            return _dbContext.QueryForObjectAsync<TDocument>(sql, param).Result;
        }

        public async Task<TDocument> QueryForObjectAsync<TDocument>(string sql, IDictionary<string, object> param = null) where TDocument : class
        {
            return await _dbContext.QueryForObjectAsync<TDocument>(sql, param);
        }

        public List<TDocument> QueryForList<TDocument>(string sql, IDictionary<string, object> param = null) where TDocument : class
        {
            return _dbContext.QueryForListAsync<TDocument>(sql, param).Result;
        }

        public async Task<List<TDocument>> QueryForListAsync<TDocument>(string sql, IDictionary<string, object> param = null) where TDocument : class
        {
            return await _dbContext.QueryForListAsync<TDocument>(sql, param);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                _dbContext.Dispose();
            }
            disposed = true;
        }
    }
}
