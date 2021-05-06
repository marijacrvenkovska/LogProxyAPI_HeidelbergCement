using LogProxyAPI.AirTableService.AirTableService.LogMessages;
using LogProxyAPI.Core.ApiModels.Request;
using LogProxyAPI.Core.ApiModels.Response;
using LogProxyAPI_HeidelbergCement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.Core.LogProxyAPI.Core
{
    // Handler for LogMessages UseCase scenario.
    public class LogMessageHandler
    {
        private readonly IAirTableLogMessageService _airTableLogMessageService;

        public LogMessageHandler(IAirTableLogMessageService airTableLogMessageService)
        {
            _airTableLogMessageService = airTableLogMessageService;
        }

        // We are Receiving and Returning DTOs instead the of the Domain Model (is represented as domain just for showcase, though in the current scenario we don't need any, as the application dosen't contain any bussiness logic), for it is a good practice unless maybe it is a basic crud operation. Returning a DTO for current action is overly complicated as the Core model suffice for the data that needs to be returned for the view and as mentioned the Application is prettly simple. 
        public async Task<ApiLogMessageDto> SendLogMessage(LogMessageDto logMessage)
        {
            // Generate the Domain model
            var airTableLogMessage = new LogMessage(logMessage.Title, logMessage.Text);

            // Call to infrastracture service.
            var response = await _airTableLogMessageService.SendLogMessage(airTableLogMessage);
            
            // We are not doing anyting with the Response. 
            // Maybe if needed we could store the log locally.

            // Since the response data dose not contain any information from the response we got back from the AirTableService, we map back directly from the Domain model.
            return new ApiLogMessageDto(airTableLogMessage.Id.ToString(), airTableLogMessage.Summary, airTableLogMessage.Message, airTableLogMessage.ReceivedAt);
        }

        public async Task<List<ApiLogMessageDto>> GetLogMessages()
        {
            var response = await _airTableLogMessageService.GetLogMessages();

            List<ApiLogMessageDto> logMessages = new List<ApiLogMessageDto>();

            // Convert Response to DTOs
            foreach (var log in response.ToList())
            {
                // Try parse the time to DateTime
                var hasParsedTime = DateTime.TryParse(log.Fields.ReceivedAt, out DateTime date);

                // We could make a log if date format is wrong.

                logMessages.Add(new ApiLogMessageDto(log.Fields.Id, log.Fields.Summary, log.Fields.Message, date));
            }

            return logMessages;
        }
    }
}
