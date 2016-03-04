using System;
using Serilog.Configuration;
using Serilog.Events;

namespace Serilog
{
    public static class LoggerConfigurationRethinkDbExtension
    {
        public static LoggerConfiguration RethinkDb(
           this LoggerSinkConfiguration loggerConfiguration,
           string databaseName = null,
           string tableName = null,
           LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
           int batchPostingLimit = 50,
           TimeSpan? period = null)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");

            var rethinkDbSink = new RethinkDbSink(
                databaseName ?? RethinkDbSink.DefaultDbName,
                tableName ?? RethinkDbSink.DefaultTableName,
                batchPostingLimit,
                period?? RethinkDbSink.DefaultPeriod
            );

            return loggerConfiguration.Sink(rethinkDbSink, restrictedToMinimumLevel);
        }
    }
}
