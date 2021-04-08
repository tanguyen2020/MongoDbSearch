using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FactoryConnection.BaseObject;
using FactoryConnection.ConnectionFactory;

namespace ExcuteService
{
    public class ServiceBase
    {
        private readonly IConnection _connection;
        public ServiceBase(IConnection connection)
        {
            _connection = connection;
        }
        public ServiceBase()
        {

        }
        public async Task<IEnumerable<DataObject>> QueryForList(string query, DataObject dataObject)
        {
            using (var _conn = _connection.CreateConnection())
            {
                ITransaction transaction = _conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    var data = await _conn.QueryForList(query, dataObject);
                    transaction.Commit();
                    return data;
                }
                catch
                {
                    transaction?.RollBack();
                    return null;
                }
                finally { }
            }
        }
        public async Task<IEnumerable<DataObject>> QueryForList(string query, IDictionary<string, object> dataObject) => await QueryForList(query, dataObject);
        public async Task<DataObject> QueryForObject(string query, DataObject dataObject)
        {
            using (var _conn = _connection.CreateConnection())
            {
                ITransaction transaction = _conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    var data = await _conn.QueryForObject(query, dataObject);
                    transaction.Commit();
                    return data;
                }
                catch
                {
                    transaction?.RollBack();
                    return null;
                }
                finally { }
            }
        }
        public async Task<DataObject> QueryForObject(string query, IDictionary<string, object> dataObject) => await QueryForObject(query, dataObject);
    }
}
