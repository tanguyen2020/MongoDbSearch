using BaseObject.DataObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFConnection
{
    public interface IGenericContext<TContext> where TContext : DbContext, IDisposable
    {
        DbSet<TEntity> Repository<TEntity>() where TEntity : class;

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// <para>
        /// Returns:
        ///         The number of state entries written to the database.
        /// </para>
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// /// <summary>
        /// Saves all changes made in this context to the database.
        /// <para>
        /// Returns:
        ///         A task that represents the asynchronous save operation. The task result contains
        ///         the number of state entries written to the database
        /// </para>
        /// </summary>
        /// <returns></returns>
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Execute a single-row query.
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
        TDocument QueryForObject<TDocument>(string sql, bool executeStored = false, IDictionary<string, object> param = null);

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
        Task<TDocument> QueryForObjectAsync<TDocument>(string sql, bool executeStored = false, IDictionary<string, object> param = null);

        /// <summary>
        /// Execute a single-row query.
        /// <para>
        /// Returns:
        ///         A sequence of data of DataObject.
        /// </para>
        /// </summary>
        /// <typeparam name="DataObject"></typeparam>
        /// <param name="sql"></param>
        /// <param name="executeStored"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        DataObject QueryForObject(string sql, bool executeStored = false, IDictionary<string, object> param = null);

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// <para>
        /// Returns:
        ///         A sequence of data of DataObject
        /// </para>
        /// </summary>
        /// <typeparam name="DataObject"></typeparam>
        /// <param name="sql"></param>
        /// <param name="executeStored"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<DataObject> QueryForObjectAsync(string sql, bool executeStored = false, IDictionary<string, object> param = null);

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
        IEnumerable<TDocument> QueryForList<TDocument>(string sql, bool executeStored = false, IDictionary<string, object> param = null);

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
        Task<IEnumerable<TDocument>> QueryForListAsync<TDocument>(string sql, bool executeStored = false, IDictionary<string, object> param = null);

        /// <summary>
        /// Execute a query asynchronously using Task.
        /// <para>
        /// Returns:
        ///         A sequence of data of IEnumerable<DataObject>
        /// </para>
        /// </summary>
        /// <typeparam name="DataObject"></typeparam>
        /// <param name="sql"></param>
        /// <param name="executeStored"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<DataObject> QueryForList(string sql, bool executeStored = false, IDictionary<string, object> param = null);

        /// <summary>
        /// Execute a query asynchronously using Task.
        /// <para>
        /// Returns:
        ///         A sequence of data of IEnumerable<DataObject>
        /// </para>
        /// </summary>
        /// <typeparam name="DataObject"></typeparam>
        /// <param name="sql"></param>
        /// <param name="executeStored"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<IEnumerable<DataObject>> QueryForListAsync(string sql, bool executeStored = false, IDictionary<string, object> param = null);
    }
}
