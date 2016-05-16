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

                if (input.ToUpperInvariant().StartsWith("BLABBER"))
                    Blabbermouth.Start(log);

                var level = input.Substring(0, input.IndexOf(" "));
                var message = input.Substring(input.IndexOf(" ") + 1);
                Producer.LogMessage(level, message, log);
            }

            log.Information("exiting - bummer..");
        }

       
    }
}
