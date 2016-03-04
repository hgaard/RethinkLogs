using Microsoft.AspNet.SignalR;
using RethinkDb.Driver;

namespace RethinkLogs
{
    public static class LogUpdateHandler
    {
        public static RethinkDB R = RethinkDB.R;

        public static void HandleUpdates()
        {

            var hub = GlobalHost.ConnectionManager.GetHubContext<LogHub>();
            var connection = R.Connection().Connect();
            var feed = R.Db(Constants.LoggingDatabase).Table(Constants.LoggingTable)
                              .Changes().RunChanges<LogEvent>(connection);

            foreach (var message in feed)
                hub.Clients.All.onMessage(
                    message.NewValue.Id,
                    message.NewValue.Message,
                    message.NewValue.Timestamp,
                    message.NewValue.Level);
        }

    }
}
