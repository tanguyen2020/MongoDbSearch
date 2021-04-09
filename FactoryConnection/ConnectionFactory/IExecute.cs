using BaseObject.DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ADOConnection.ConnectionFactory
{
    public interface IExecute: IDisposable
    {
        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// <para>
        /// Returns:
        ///         A sequence of data of DataObject
        ///</para>
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<DataObject> QueryForObject(string statement, DataObject param = null);

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// <para>
        /// Returns:
        ///         A sequence of data of DataObject
        ///</para>
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<DataObject> QueryForObject(string statement, IDictionary<string, object> param = null);

        /// <summary>
        /// Execute a query asynchronously using Task
        /// <para>
        /// Returns:
        ///         A sequence of data of DataObject
        /// </para>
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<IEnumerable<DataObject>> QueryForList(string statement, DataObject param = null);

        /// <summary>
        /// Execute a query asynchronously using Task
        /// <para>
        /// Returns:
        ///         A sequence of data of DataObject
        /// </para>
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<IEnumerable<DataObject>> QueryForList(string statement, IDictionary<string, object> param = null);

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// <para>
        /// Returns:
        ///         A sequence of data of TDocument
        ///</para>
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="statement"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<TDocument> QueryForObject<TDocument>(string statement, DataObject param = null);

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// <para>
        /// Returns:
        ///         A sequence of data of TDocument
        ///</para>
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="statement"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<TDocument> QueryForObject<TDocument>(string statement, IDictionary<string, object> param = null);

        /// <summary>
        /// Execute a query asynchronously using Task
        /// <para>
        /// Returns:
        ///         A sequence of data of TDocument; if a basic type (int, string, etc) is queried then the 
        ///         data from the first column in assumed, otherwise an instance is created per row, 
        ///         and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </para>
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<IEnumerable<TDocument>> QueryForList<TDocument>(string statement, DataObject param = null);

        /// <summary>
        /// Execute a query asynchronously using Task
        /// <para>
        /// Returns:
        ///         A sequence of data of TDocument; if a basic type (int, string, etc) is queried then the 
        ///         data from the first column in assumed, otherwise an instance is created per row, 
        ///         and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </para>
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<IEnumerable<TDocument>> QueryForList<TDocument>(string statement, IDictionary<string, object> param = null);

        ITransaction BeginTransaction(IsolationLevel isolationLevel);
    }
}
