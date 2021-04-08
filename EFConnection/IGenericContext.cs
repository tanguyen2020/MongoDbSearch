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
        TDocument QueryForObject<TDocument>(string spname, IDictionary<string, object> param = null) where TDocument : class;
        Task<TDocument> QueryForObjectAsync<TDocument>(string spname, IDictionary<string, object> param = null) where TDocument : class;
        List<TDocument> QueryForList<TDocument>(string spname, IDictionary<string, object> param = null) where TDocument : class;
        Task<List<TDocument>> QueryForListAsync<TDocument>(string spname, IDictionary<string, object> param = null) where TDocument : class;
    }
}
