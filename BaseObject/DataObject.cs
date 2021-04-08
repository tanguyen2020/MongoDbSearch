using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace BaseObject.DataObject
{
    public class DataObject : object, IDataObject
    {
        public IDictionary<string, object> Data = new Dictionary<string, object>();
        public DataObject()
        {
            Data.Clear();
        }
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        ~DataObject() => new DataObject();

        public void Add(string key, object value)
        {
            Data.Add(key, value);
        }
        public void Remove(string key)
        {
            Data.Remove(key);
        }
        public object this[string key]
        {
            get
            {
                object objValue;
                return Data.TryGetValue(key, out objValue) ? objValue : null;
            }
            set
            {
                Data.Add(key, value);
            }
        }
        public void CopyModel<T>(T model)
        {
            Type objectType = model.GetType();
            object value;
            foreach (var memberInfo in objectType.GetProperties())
            {
                try
                {
                    value = memberInfo.GetValue(model, null);
                    Data.Add(memberInfo.Name, value);
                }
                catch { }
            }
        }
    }
}
