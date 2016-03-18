using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace TCService2._2.SignalR
{
 
    [HubName("arfaHub")]
    public class ArfaHub : Hub
    {
        [HubMethodName("hello")]
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}