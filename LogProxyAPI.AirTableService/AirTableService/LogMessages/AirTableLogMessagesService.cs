using LogProxyAPI_HeidelbergCement.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static LogProxyAPI.AirTableService.AirTableService.LogMessages.GetAirTableLogMessage;

namespace LogProxyAPI.AirTableService.AirTableService.LogMessages
{
    // Service Responsible for communication with the AirTable third pary service. 
    // Responsible only for sending and parsing back the response.
    public class AirTableLogMessagesService : IAirTableLogMessageService
    {
        public readonly string SendLogMessageUrl;
        public readonly string GetLogMessagesUrl;
        public readonly string _apiKey;

        public AirTableLogMessagesService(IOptions<AirTableApiSettings> airTableSettings)
        {
            _apiKey = airTableSettings.Value.ApiKey;
            // We could have the Different routes configured in the settings.
            SendLogMessageUrl = airTableSettings.Value.AirTableApiUrl + "/appD1b1YjWoXkUJwR/Messages";
            GetLogMessagesUrl = airTableSettings.Value.AirTableApiUrl + "/appD1b1YjWoXkUJwR/Messages";
        }

        public async Task<Record> SendLogMessage(LogMessage logMessage)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

                //httpClient.DefaultRequestHeaders.Add("ApiKey", _apiKey);
                var url = new Uri(SendLogMessageUrl);

                var message = JsonSerializer.Serialize(new PostAirTableLogMessage(logMessage));
                StringContent content = new StringContent(message);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var response = await httpClient.PostAsync(url, content);

                var savedLogMessageContent = await response.Content.ReadAsStringAsync();

                var savedLogMessages = JsonSerializer.Deserialize<GetAirTableLogMessage>(savedLogMessageContent);

                return savedLogMessages.Records.First();
            }
        }
        // Example scenario for possible results we could return.
        public async Task<AirTableLogMessageResponse<Record>> SendLogMessageWithResult(LogMessage logMessage)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
                    
                    //httpClient.DefaultRequestHeaders.Add("ApiKey", _apiKey);
                    var url = new Uri(SendLogMessageUrl);

                    var message = JsonSerializer.Serialize(new PostAirTableLogMessage(logMessage));
                    StringContent content = new StringContent(message);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await httpClient.PostAsync(url, content);
                    //response.EnsureSuccessStatusCode();

                    var savedLogMessageContent = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        return AirTableLogMessageResponse<Record>.CreateStatusResponseFailure((int)response.StatusCode, savedLogMessageContent);
                    }

                    var savedLogMessages = JsonSerializer.Deserialize<GetAirTableLogMessage>(savedLogMessageContent);

                    return AirTableLogMessageResponse<Record>.CreateSuccess(savedLogMessages.Records.First());
                }
                catch (HttpRequestException exception)
                {
                    return AirTableLogMessageResponse<Record>.CreatRequestSendFailure(exception.Message);
                    throw exception;
                }
                catch (JsonException exception)
                {
                    return AirTableLogMessageResponse<Record>.CreatJsonParseFailure(exception.Message);
                    throw exception;
                }
            }
        }

        public async Task<IEnumerable<Record>> GetLogMessages()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
                    //httpClient.DefaultRequestHeaders.Add("ApiKey", _apiKey);

                    var url = new Uri(SendLogMessageUrl);

                    var response = await httpClient.GetAsync(url);

                    var savedLogMessageContent = await response.Content.ReadAsStringAsync();
                    var savedLogMessages = JsonSerializer.Deserialize<GetAirTableLogMessage>(savedLogMessageContent);

                    return savedLogMessages.Records;
                }

                catch (HttpRequestException exception)
                {
                    throw exception;
                }
                catch (JsonException exception)
                {
                    throw exception;
                }
            }
        }
    }
}
