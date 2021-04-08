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

        public TDocument QueryForObject<TDocument>(string spname, IDictionary<string, object> param = null) where TDocument : class
        {
            return _dbContext.QueryForObjectAsync<TDocument>(spname, param).Result;
        }

        public async Task<TDocument> QueryForObjectAsync<TDocument>(string spname, IDictionary<string, object> param = null) where TDocument : class
        {
            return await _dbContext.QueryForObjectAsync<TDocument>(spname, param);
        }

        public List<TDocument> QueryForList<TDocument>(string spname, IDictionary<string, object> param = null) where TDocument : class
        {
            return _dbContext.QueryForListAsync<TDocument>(spname, param).Result;
        }

        public async Task<List<TDocument>> QueryForListAsync<TDocument>(string spname, IDictionary<string, object> param = null) where TDocument : class
        {
            return await _dbContext.QueryForListAsync<TDocument>(spname, param);
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
