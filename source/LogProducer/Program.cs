using System;
using RethinkDb.Driver;
using Serilog;

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

            while (true)
            {
                var input = Console.ReadLine();
                if (input == ":q")
                    break;

                if (input.ToUpperInvariant().StartsWith("BLA"))
                    Blabbermouth.Start(log);

                var level = GetLevel(input);
                var message = input.Substring(input.IndexOf(" ") + 1);
                Producer.LogMessage(level, message, log);
            }

            log.Information("exiting - bummer..");
        }

        private static int GetLevel(string input)
        {
            var level = input.Substring(0, input.IndexOf(" "));
            if (string.IsNullOrEmpty(level))
                return 1;

            if (level.ToUpperInvariant().StartsWith("FA"))
                return 5;

            if (level.ToUpperInvariant().StartsWith("ER"))
                return 4;

            if (level.ToUpperInvariant().StartsWith("WA"))
                return 3;

            if (level.ToUpperInvariant().StartsWith("IN"))
                return 2;

            
            return 1;
            
        }
    }
}
