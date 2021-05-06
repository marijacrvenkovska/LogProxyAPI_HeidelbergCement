using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.Core.ApiModels.Response
{
    // DTO for client response data
    public class ApiLogMessageDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime ReceivedAt { get; set; }

        public ApiLogMessageDto(string id, string title, string message, DateTime receivedAt)
        {
            Id = id;
            Title = title;
            Message = message;
            ReceivedAt = receivedAt;
        }
    }
}
