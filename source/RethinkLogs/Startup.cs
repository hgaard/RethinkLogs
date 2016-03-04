using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using RethinkLogs;

[assembly: OwinStartup(typeof(Startup))]

namespace RethinkLogs
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            LogHub.Init();

            Task.Factory.StartNew(
                LogUpdateHandler.HandleUpdates,
                TaskCreationOptions.LongRunning);

            app.MapSignalR();
        }
    }
}