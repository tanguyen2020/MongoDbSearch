using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using BaseObject.DataObject;
using System.Linq;
using Dapper;

namespace EFConnection
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<T> Repository<T>() where T : class
        {
            return base.Set<T>();
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// <para>
        /// Returns:
        ///         The number of state entries written to the database.
        /// </para>
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// <para>
        /// Returns:
        ///         A task that represents the asynchronous save operation. The task result contains
        ///         the number of state entries written to the database
        /// </para>
        /// </summary>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// <para>
        /// Returns:
        ///         A sequence of data of TDocument
        /// </para>
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="sql"></param>
        /// <param name="executeStored"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<TDocument> QueryForObjectAsync<TDocument>(string sql, bool executeStored = false, IDictionary<string, object> param = null)
        {
            return await base.Database.GetDbConnection().QueryFirstAsync<TDocument>(executeStored ? $"Exec {sql}" : sql, ParseParameters(param));
        }

        /// <summary>
        /// Execute a query asynchronously using Task.
        /// <para>
        /// Returns:
        ///         A sequence of data of TDocument
        /// </para>
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="sql"></param>
        /// <param name="executeStored"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TDocument>> QueryForListAsync<TDocument>(string sql, bool executeStored = false, IDictionary<string, object> param = null)
        {
            return await base.Database.GetDbConnection().QueryAsync<TDocument>(executeStored ? $"Exec {sql}" : sql, ParseParameters(param));
        }

        public Dictionary<string, object> ParseParameters(IDictionary<string, object> param)
        {
            var parameters = new Dictionary<string, object>();
            foreach (var i in param)
            {
                parameters.Add($"@{i.Key}", i.Value ?? DBNull.Value);
            }
            return parameters;
        }
    }
}
