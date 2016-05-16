using System;
using Serilog;

namespace RethinkLogs.LogProducer
{
    public class Producer
    {
        public static void LogMessage(string level, string message, ILogger log)
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