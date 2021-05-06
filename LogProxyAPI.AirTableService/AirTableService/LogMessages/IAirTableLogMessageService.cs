using LogProxyAPI_HeidelbergCement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static LogProxyAPI.AirTableService.AirTableService.LogMessages.GetAirTableLogMessage;

namespace LogProxyAPI.AirTableService.AirTableService.LogMessages
{
    public interface IAirTableLogMessageService
    {
        Task<Record> SendLogMessage(LogMessage logMessage);
        Task<AirTableLogMessageResponse<Record>> SendLogMessageWithResult(LogMessage logMessage);
        Task<IEnumerable<Record>> GetLogMessages();
    }
}
