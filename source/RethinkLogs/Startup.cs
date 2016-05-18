using System.Threading.Tasks;
using Owin;


namespace RethinkLogs
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            LogHub.Init();

            Task.Factory.StartNew(
                LogHub.HandleUpdates,
                TaskCreationOptions.LongRunning);

            app.MapSignalR();
        }
    }
}