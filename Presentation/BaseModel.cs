using System;
using System.Collections.Generic;
using System.Reflection;

namespace Presentation
{
    public class BaseModel
    {
        /// <summary>
        /// Clear all value of the object to null
        /// </summary>
        public void Reset()
        {
            PropertyInfo propertyInfo;
            foreach (var memberInfo in this.GetType().GetProperties())
            {
                try
                {
                    propertyInfo = this.GetType().GetProperty(memberInfo.Name);
                    propertyInfo.SetValue(this, null);
                }
                catch { }
            }
        }

        /// <summary>
        /// Creates a shallow copy of the current System.Object.
        /// <para>
        /// Returns: A shallow copy of the current System.Object.
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ShallowCopy<T>()
        {
            return (T)this.MemberwiseClone();
        }

        /// <summary>
        /// Copy value from object A to object B must be the same of the PropertyName and DataType
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void CopyFrom<T>(T model)
        {
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in this.GetType().GetProperties())
            {
                try
                {
                    propertyInfo = this.GetType().GetProperty(memberInfo.Name);
                    value = memberInfo.GetValue(model, null);
                    propertyInfo.SetValue(this, value);
                }
                catch { }
            }
        }

        /// <summary>
        /// Get all value object to Dictionary
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> AllDictionary()
        {
            var keyValues = new Dictionary<string, object>();
            PropertyInfo propertyInfo;
            foreach (var memberInfo in this.GetType().GetProperties())
            {
                try
                {
                    propertyInfo = this.GetType().GetProperty(memberInfo.Name);
                    keyValues.Add(propertyInfo.Name, memberInfo.GetValue(this));
                }
                catch { }
            }
            return keyValues;
        }
    }
}
