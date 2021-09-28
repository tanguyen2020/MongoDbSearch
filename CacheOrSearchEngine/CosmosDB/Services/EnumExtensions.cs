using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Caching.Services
{
    public enum StatusReleaseDate
    {
        [Description("New")]
        New,
        [Description("Updated")]
        Updated,
        [Description("In Progress")]
        InProgress
    }
    public static class EnumExtensions
    {
        public static string Description(this Enum value)
        {
            if (value == null) return string.Empty;

            var attribute = value
                .GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                .GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute.Description;
        }
    }
}
