using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FastqlSample.Infrastructure.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// Returns DescriptionAttribute value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description
                ?? value.ToString();
        }

        public static string GetValue(this Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<ValueAttribute>()
                    ?.Value
                ?? value.ToString();
        }


    }

    public class ValueAttribute : Attribute
    {
        public ValueAttribute(string value)
        {
            Value = value;
        }
        public string Value { get; set; }
    }
}
