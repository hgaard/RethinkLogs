using System.Threading;
using Serilog;

namespace RethinkLogs.LogProducer
{
    public class Blabbermouth
    {
        //private bool _continue = true;

        public static void Start(ILogger logger)
        {
            while (true)
            {
                //Gossip.Tell().Important().Gossip.ToProducer();
                Producer.LogMessage(Gossip.HowImportant(), Gossip.Tell(), logger);

                Thread.Sleep(Gossip.Wait());
            }
        }

    }
}
