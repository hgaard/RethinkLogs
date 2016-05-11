using System;
using RethinkDb.Driver;
using Serilog;
using Serilog.Core;

namespace RethinkLogs.LogProducer
{
    class Program
    {
        public static RethinkDB R = RethinkDB.R;
        static void Main(string[] args)
        {

            var log = new LoggerConfiguration()
                 .WriteTo.RethinkDB()
                 .Enrich.WithProperty("Application", "Producer")
                 .Enrich.WithThreadId()
                 .Enrich.WithEnvironmentUserName()
                 .MinimumLevel.Verbose()
                .CreateLogger();

            log.Information("Hi there - starting awesome app");
            var goOn = true;
            while (goOn)
            {
                var input = Console.ReadLine();
                if (input == ":q")
                    goOn = false;

                var level = input.Substring(0, input.IndexOf(" "));
                var message = input.Substring(input.IndexOf(" ") + 1);
                LogMessage(level, message, log);
            }

            log.Information("exiting - bummer..");
        }

        private static void LogMessage(string level, string message, ILogger log)
        {
            if (level.ToUpperInvariant().StartsWith("FA"))
                log.Fatal(new ArgumentException(message), message, Environment.OSVersion);
            else if (level.ToUpperInvariant().StartsWith("ER"))
                log.Error(message, Environment.CommandLine);
            else if (level.ToUpperInvariant().StartsWith("WA"))
                log.Warning(message);
            else if (level.ToUpperInvariant().StartsWith("IN"))
                log.Information(message);
            else
            {
                log.Verbose(message);
            }
        }
    }
}
