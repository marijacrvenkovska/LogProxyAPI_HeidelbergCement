using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.Core.ApiModels.Request
{
    // DTO for client request Data
    public class LogMessageDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
