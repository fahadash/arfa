using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCService2._2.SignalR
{
    public class ArfaInterface
    {
        public static void TableAdded(int tableId, string tableName)
        {
            var context = GlobalHost
                .ConnectionManager.GetHubContext<ArfaHub>();

            context.Clients.All.tableAdded(tableId, tableName);
        }
    }
}