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
                .CreateLogger();

            log.Information("Hi there - starting awesome app");
            var goOn = true;
            while (goOn)
            {
                var input = Console.ReadLine();
                if (input == ":q")
                    goOn = false;
                
                log.Information(input);
            }

            log.Information("exiting - bummer..");
        }
    }
}
