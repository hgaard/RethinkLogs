using System;
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

        public JArray History(int limit)
        {
            var output = R.Db(Constants.LoggingDatabase).Table(Constants.LoggingTable)
                .OrderBy(R.Desc("Timestamp"))
                .Limit(limit)
                .OrderBy("Timestamp")
                .RunResult<JArray>(_connection);

            return output;
        }

        public JArray Query(string queryString)
        {
            var result = new JArray();
            try
            {
                result = R.Db(Constants.LoggingDatabase).Table(Constants.LoggingTable)
                   .Filter(r => r["Message"].Match("(?i)" + queryString))
                   .OrderBy(R.Desc("Timestamp"))
                   .RunResult<JArray>(_connection);
            }
            catch (Exception)
            {
                // ignore
            }
            return result;
        }

        public JObject Get(string id)
        {
            var result =
                R.Db(Constants.LoggingDatabase).Table(Constants.LoggingTable)
                .Get(id).RunResult<JObject>(_connection);

            return result;
        }
    }
}