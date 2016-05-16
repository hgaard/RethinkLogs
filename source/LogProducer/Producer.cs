using System;
using System.Runtime.Remoting.Channels;
using Serilog;

namespace RethinkLogs.LogProducer
{
    public class Producer
    {
        public static void LogMessage(int level, string message, ILogger log)
        {
            switch (level)
            {
                case 5:
                    log.Fatal(new ArgumentException(message), message, Environment.OSVersion);
                    break;
                case 4:
                    log.Error(message, Environment.CommandLine);
                    break;
                case 3:
                    log.Warning(message);
                    break;
                case 2:
                    log.Information(message);
                    break;
                default:
                    log.Verbose(message);
                    break;

            }   
        }
    }
}