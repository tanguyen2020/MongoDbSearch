using System;
using System.Collections.Generic;
using System.Text;

namespace BaseObject.DataObject
{
    public interface IDataObject
    {
        object this[string key] { get; }
        void Add(string key, object value);
        void Remove(string key);
        void CopyModel<T>(T model);
    }
}
