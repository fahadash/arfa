using arfa.Interface.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace arfa.Interface.Helpers
{
    public static class AttributeHelper
    {
        public static string GetDescription<T>(this T obj) 
        {
            var attribute = obj.GetType().GetTypeInfo().GetCustomAttribute<DescriptionAttribute>();


            return attribute.Description;
        }
    }
}
