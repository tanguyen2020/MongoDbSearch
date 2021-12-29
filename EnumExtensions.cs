using System;
using System.ComponentModel;

namespace NotificationProcessor.Core
{
    public static class Extension
    {
        // Get value enum from description
        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            return default(T);
        }
    }
    
    // Get Description of the enum
    public static string GetDescription<T>(this T item) where T : struct
        {
            Type type = item.GetType();
            MemberInfo[] memberInfo = type.GetMember(item.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return item.ToString();
        }
}
