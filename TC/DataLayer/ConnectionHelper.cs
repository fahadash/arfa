using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace TC.DataLayer
{
    class ConnectionHelper
    {
        public static string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["TCData"].ConnectionString;
        }
    }
}
