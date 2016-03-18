using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(TCService2._2.SignalR.ArfaStartup))]

namespace TCService2._2.SignalR
{
    public class ArfaStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);

                var hubConfiguration = new HubConfiguration()
                {

                };

                map.RunSignalR(hubConfiguration);
            });
            
        }
    }
}
