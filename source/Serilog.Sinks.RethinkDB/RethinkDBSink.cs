using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RethinkDb.Driver;
using RethinkDb.Driver.Net;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;

namespace Serilog
{
    public class RethinkDbSink : PeriodicBatchingSink
    {
        private readonly string _databaseName;
        private readonly string _tableName;
       private static readonly RethinkDB R = RethinkDB.R;
        private readonly Connection _connection;
        
        /// <summary>
        /// A reasonable default time to wait between checking for event batches.
        /// </summary>
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        /// <summary>
        /// The default name for the logging database.
        /// </summary>
        public static readonly string DefaultDbName = "logging";

        /// <summary>
        /// The default name for the log table.
        /// </summary>
        public static readonly string DefaultTableName = "log";

        public RethinkDbSink(string databaseName,
           string tableName,
           int batchPostingLimit,
           TimeSpan period) : base(batchPostingLimit, period)
        {
            _databaseName = databaseName;
            _tableName = tableName;
            _connection = R.Connection().Connect();
        }

        protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            EnsureDatabaseAndTable();

            foreach (var logEvent in events)
            {
                await R.Db(_databaseName).Table(_tableName).Insert(new RethinkDbLogEvent
                {
                    Id = Guid.NewGuid(),
                    Timestamp = logEvent.Timestamp,
                    Message = logEvent.RenderMessage(),
                    MessageTemplate = logEvent.MessageTemplate.Text,
                    Level = logEvent.Level,
                    Exception = logEvent?.Exception?.ToString(),
                    Properties = logEvent.Properties.ToDictionary<KeyValuePair<string, LogEventPropertyValue>, string, object>(k => k.Key, v => v.Value)
                }).RunAsync(_connection);
            }
        }

        private async void EnsureDatabaseAndTable()
        {
            var dbExists = await R.DbList().Contains(_databaseName).RunAsync(_connection);
            if (!dbExists)
                R.DbCreate(_databaseName);

            var tableExists = await R.Db(_databaseName).TableList().Contains(_tableName).RunAsync(_connection);
            
            if (!tableExists)
                R.Db(_databaseName).TableCreate(_tableName);
        }
    }
}
