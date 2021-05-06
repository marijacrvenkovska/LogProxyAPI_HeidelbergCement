using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LogProxyAPI.AirTableService.AirTableService.LogMessages
{
    // LogMessage Response model for Get
    public class GetAirTableLogMessage
    {
        [JsonPropertyName("records")]
        public List<Record> Records { get; set; }

        public class Record
        {
            public string id { get; set; }
            [JsonPropertyName("fields")]
            public Fields Fields { get; set; }
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
