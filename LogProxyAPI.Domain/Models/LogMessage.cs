using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI_HeidelbergCement.Models
{
    public class LogMessage
    {
        public LogMessage(string summary, string message)
        {
            Id = Guid.NewGuid();
            Summary = summary;
            Message = message;
            ReceivedAt = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public string Summary { get; set; }
        public string Message { get; set; }
        public DateTime ReceivedAt { get; set; }
    }
}
