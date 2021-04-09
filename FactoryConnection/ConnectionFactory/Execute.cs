using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using BaseObject.DataObject;

namespace ADOConnection.ConnectionFactory
{
    public abstract class Execute : IExecute
    {
        private bool disposed = false;
        private DbConnection dbConnection;
        public Execute(string connectionString)
        {
            dbConnection = CreateConnection(connectionString);
            dbConnection.Open();
        }
        public abstract DbConnection CreateConnection(string connectionString);
        public abstract DbDataAdapter CreateAdapter(DbCommand dbCommand);
        public ITransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return new Transaction(dbConnection.BeginTransactionAsync(isolationLevel).Result);
        }

        public async Task<DataObject> QueryForObject(string statement, DataObject param = null)
        {
            return await dbConnection.QuerySingleAsync<DataObject>(statement, param);
        }

        public async Task<TDocument> QueryForObject<TDocument>(string statement, DataObject param = null)
        {
            return await dbConnection.QuerySingleAsync<TDocument>(statement, param);
        }

        public async Task<DataObject> QueryForObject(string statement, IDictionary<string, object> param = null) => await QueryForObject(statement, param);

        public async Task<TDocument> QueryForObject<TDocument>(string statement, IDictionary<string, object> param = null) => await QueryForObject<TDocument>(statement, param);

        public async Task<IEnumerable<DataObject>> QueryForList(string statement, DataObject param = null)
        {
            return await dbConnection.QueryAsync<DataObject>(statement, param);
        }

        public async Task<IEnumerable<TDocument>> QueryForList<TDocument>(string statement, DataObject param = null)
        {
            return await dbConnection.QueryAsync<TDocument>(statement, param);
        }

        public async Task<IEnumerable<DataObject>> QueryForList(string statement, IDictionary<string, object> param = null) => await QueryForList(statement, param);

        public async Task<IEnumerable<TDocument>> QueryForList<TDocument>(string statement, IDictionary<string, object> param = null) => await QueryForList<TDocument>(statement, param);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
            disposed = true;
        }

        ~Execute() => Dispose(false);
    }
}
