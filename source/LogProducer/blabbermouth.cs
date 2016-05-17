using System;
using System.Collections.Generic;
using System.Threading;
using Serilog;

namespace RethinkLogs.LogProducer
{
    public class Blabbermouth
    {
        private static readonly Random Rnd = new Random(DateTime.Now.Millisecond);
        private static readonly List<string> ImportantMessages = new List<string>(new[]
            {"Goodbye cruel world",
            "Really hope you enjoyed that one",
            "This is the beginning of something big",
            "I should be able to come up with great messages",
            "All those moments will be lost in time… like tears in rain.",
            "But then I realised maybe that's what hell is: the entire rest of eternity spent in f_____g Bruges. And I really really hoped I wouldn't die.",
            "Did you think I'd be too stupid to know what a eugoogly is?",
            "May the Force be with you.",

        });
        

        public static void Start(ILogger logger)
        {
            while (true)
            {
                Producer.LogMessage(HowImportant(), Tell(), logger);

                Thread.Sleep(Wait());
            }
        }

        public static void Stop()
        {
            // Do shit with cancellation token!
        }

        public static int HowImportant()
        {
            return Rnd.Next(1, 5);
        }

        public static string Tell()
        {
            return ImportantMessages[Rnd.Next(ImportantMessages.Count - 1)];
        }

        public static int Wait()
        {
            return Rnd.Next(200, 2000);
        }

    }
}
