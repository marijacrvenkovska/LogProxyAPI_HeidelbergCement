using LogProxyAPI_HeidelbergCement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LogProxyAPI.AirTableService.AirTableService.LogMessages
{
    // LogMessage Response model for Post
    // Different of get because some Properties are omit.
    public class PostAirTableLogMessage
    { 
        internal PostAirTableLogMessage(LogMessage logMessage)
        {
            Records = new List<Record> { new Record { Fields = new Fields
            {
                Id = logMessage.Id.ToString(),
                Message = logMessage.Message,
                Summary = logMessage.Summary,
                ReceivedAt = logMessage.ReceivedAt.ToString("o")
            } } };
        }

        [JsonPropertyName("records")]
        public List<Record> Records { get; set; }

        public class Record
        {
            [JsonIgnore]
            public string id { get; set; }
            [JsonPropertyName("fields")]
            public Fields Fields { get; set; }
            [JsonIgnore]
            public DateTime createdTime { get; set; }
        }

        public class Fields
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }
            public string Summary { get; set; }
            public string Message { get; set; }
            [JsonPropertyName("receivedAt")]
            public string ReceivedAt { get; set; }
        }
    }
}
