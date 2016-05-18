using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using RethinkDb.Driver;
using RethinkDb.Driver.Net;

namespace RethinkLogs
{
    public class LogHub : Hub
    {
        private static readonly RethinkDB R = RethinkDB.R;
        private static Connection _connection;

        internal static void Init()
        {
            _connection = R.Connection().Connect();
        }

        public static void HandleUpdates()
        {

            var hub = GlobalHost.ConnectionManager.GetHubContext<LogHub>();
            var connection = R.Connection().Connect();
            var feed = R.Db(Constants.LoggingDatabase)
                        .Table(Constants.LoggingTable)
                        .Changes()
                        .RunChanges<LogEvent>(connection);


            feed.Select(x => hub.Clients.All.onMessage(x.NewValue)).ToList();
        }

        public IList<LogEvent> History(int limit)
        {
            var output = R.Db(Constants.LoggingDatabase).Table(Constants.LoggingTable)
                .OrderBy(R.Desc("Timestamp"))
                .Limit(limit)
                .OrderBy("Timestamp")
                .RunResult<IList<LogEvent>>(_connection);

            return output;
        }

        public IList<LogEvent> Query(string queryString)
        {
            try
            {
                return R.Db(Constants.LoggingDatabase).Table(Constants.LoggingTable)
                   .Filter(r => r["Message"].Match("(?i)" + queryString))
                   .OrderBy(R.Desc("Timestamp"))
                   .RunResult<IList<LogEvent>>(_connection);
            }
            catch (Exception)
            {
                // ignore
            }
            return null;
        }

        public LogEvent Get(string id)
        {
            return
                R.Db(Constants.LoggingDatabase).Table(Constants.LoggingTable)
                .Get(id).RunResult<LogEvent>(_connection);
        }
    }
}