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

        public TDocument QueryForObject<TDocument>(string sql, bool executeStored = false, IDictionary<string, object> param = null)
        {
            return _dbContext.QueryForObjectAsync<TDocument>(sql, executeStored, param).Result;
        }

        public async Task<TDocument> QueryForObjectAsync<TDocument>(string sql, bool executeStored = false, IDictionary<string, object> param = null)
        {
            return await _dbContext.QueryForObjectAsync<TDocument>(sql, executeStored, param);
        }

        public IEnumerable<TDocument> QueryForList<TDocument>(string sql, bool executeStored = false, IDictionary<string, object> param = null)
        {
            return _dbContext.QueryForListAsync<TDocument>(sql, executeStored, param).Result;
        }

        public async Task<IEnumerable<TDocument>> QueryForListAsync<TDocument>(string sql, bool executeStored = false, IDictionary<string, object> param = null)
        {
            return await _dbContext.QueryForListAsync<TDocument>(sql, executeStored, param);
        }

        public DataObject QueryForObject(string sql, bool executeStored = false, IDictionary<string, object> param = null)
        {
            return _dbContext.QueryForObjectAsync<DataObject>(sql, executeStored, param).Result;
        }

        public async Task<DataObject> QueryForObjectAsync(string sql, bool executeStored = false, IDictionary<string, object> param = null)
        {
            return await _dbContext.QueryForObjectAsync<DataObject>(sql, executeStored, param);
        }

        public IEnumerable<DataObject> QueryForList(string sql, bool executeStored = false, IDictionary<string, object> param = null)
        {
            return _dbContext.QueryForListAsync<DataObject>(sql, executeStored, param).Result;
        }

        public async Task<IEnumerable<DataObject>> QueryForListAsync(string sql, bool executeStored = false, IDictionary<string, object> param = null)
        {
            return await _dbContext.QueryForListAsync<DataObject>(sql, executeStored, param);
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
