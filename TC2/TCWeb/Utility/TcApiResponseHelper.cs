using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TCWeb.Utility
{

    public static class TcApiResponseHelper
    {
        public static T GetPropertyValue<T>(this TCServiceResponse response, string propertyName)
        {
            return GetPropertyValue<T>(response.response, propertyName);
        }

        public static T GetPropertyValue<T>(object obj, string propertyName)
        {

            if (obj.GetType() == typeof(JObject))
            {
                object returnObj = null;

                JObject jObj = (JObject)obj;

                if (typeof(T) == typeof(Int32))
                {
                    returnObj = Convert.ToInt32(jObj[propertyName].ToString());
                }
                else if (typeof(T) == typeof(Guid))
                {
                    returnObj = Guid.Parse(jObj[propertyName].ToString());
                }
                else if (typeof(T) == typeof(string))
                {
                    returnObj = jObj[propertyName].ToString();
                }

                return (T) returnObj;
            }

            PropertyInfo pi = obj.GetType().GetProperty(propertyName);

            if (pi == null)
            {
                return default(T);
            }

            return (T)pi.GetValue(obj);
        }
    }

}