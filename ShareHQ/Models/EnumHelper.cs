using System;
using System.Reflection;

namespace ShareHQ.Models
{
    public static class EnumHelper
    {
        public static string GetEnumStringValue(Enum value)
        {
            string output = string.Empty;
            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());

            EnumStringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(EnumStringValueAttribute), false) as EnumStringValueAttribute[];

            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }

    public class EnumStringValueAttribute : Attribute
    {
        private readonly string _value;

        public EnumStringValueAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }
}