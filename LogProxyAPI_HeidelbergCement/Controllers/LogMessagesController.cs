using LogProxyAPI.Core.ApiModels.Request;
using LogProxyAPI.Core.ApiModels.Response;
using LogProxyAPI.Core.LogProxyAPI.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace LogProxyAPI_HeidelbergCement.Controllers
{
    [ApiController]
    [Route("api/logMessage")]
    public class LogMessageController : ControllerBase
    {
        private readonly LogMessageHandler _handler;
        public LogMessageController(LogMessageHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiLogMessageDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<ApiLogMessageDto>> LogMessage([FromBody] LogMessageDto logMessage)
        {
            var loggedMessage = await _handler.SendLogMessage(logMessage);
            
            return CreatedAtAction(nameof(GetLogMessage), new { id = loggedMessage.Id }, loggedMessage);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ApiLogMessageDto>), StatusCodes.Status200OK)]
        public async Task<List<ApiLogMessageDto>> GetLogMessages()
        {
            var loggedMessages = await _handler.GetLogMessages();

            return loggedMessages;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiLogMessageDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiLogMessageDto>> GetLogMessage(string id)
        {
            var loggedMessages = await _handler.GetLogMessages();

            var loggedMessage = loggedMessages.FirstOrDefault(x => x.Id == id);

            if (loggedMessage == null)
                return NotFound();

            return Ok(loggedMessage);
        }
    }
}
