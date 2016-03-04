using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RethinkLogs
{
    public class LogEvent
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

    public enum LogEventLevel
    {
        Verbose,
        Debug,
        Information,
        Warning,
        Error,
        Fatal,
    }
}