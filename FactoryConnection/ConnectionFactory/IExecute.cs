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
        Task<DataObject> QueryForObject(string statement, DataObject param = null);
        Task<DataObject> QueryForObject(string statement, IDictionary<string, object> param = null);
        Task<IEnumerable<DataObject>> QueryForList(string statement, DataObject param = null);
        Task<IEnumerable<DataObject>> QueryForList(string statement, IDictionary<string, object> param = null);
        ITransaction BeginTransaction(IsolationLevel isolationLevel);
    }
}
