using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RethinkLogs.LogProducer
{
    
    public class Gossip
    {
        private static List<string> _importantMessages = new List<string>(new []
            {"Goodbye cruel world",
            "Really hope you enjoyed that one",
            "This is the beginning of something big",
            "I should be able to come up with great messages",
            "All those moments will be lost in time… like tears in rain.",
            "But then I realised maybe that's what hell is: the entire rest of eternity spent in f_____g Bruges. And I really really hoped I wouldn't die.",
            "Did you think I'd be too stupid to know what a eugoogly is?",
            "May the Force be with you.",

        });
        private static Random _rnd = new Random(DateTime.Now.Millisecond);

        public static string Tell()
        {
            return _importantMessages[_rnd.Next(_importantMessages.Count - 1)];
        }

        public static int Wait()
        {
            return _rnd.Next(200, 2000);
        }
    }
}
