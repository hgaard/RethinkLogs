using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Serilog.Events;

namespace Serilog
{
    public class RethinkDbLogEvent
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id;

        public DateTimeOffset Timestamp;

        public LogEventLevel Level;

        public string Message;

        public string MessageTemplate;

        public Dictionary<string, object> Properties;

        public string Exception;
    }

        
}