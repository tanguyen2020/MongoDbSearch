using BaseObject.DataObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFConnection
{
    public interface IGenericContext<T> where T: DbContext, IDisposable
    {
        DbSet<TEntity> Repository<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        TDocument QueryForObject<TDocument>(string sql, bool excuteStored = false, IDictionary<string, object> param = null);
        Task<TDocument> QueryForObjectAsync<TDocument>(string sql, bool excuteStored = false, IDictionary<string, object> param = null);
        List<TDocument> QueryForList<TDocument>(string sql, bool excuteStored = false, IDictionary<string, object> param = null);
        Task<List<TDocument>> QueryForListAsync<TDocument>(string sql, bool excuteStored = false, IDictionary<string, object> param = null);
    }
}
