using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.AirTableService.AirTableService.LogMessages
{
    // In case there could be some intended or unhandled errors, instead of throwing an exception we will return a WrapperResult model so we can leave the handling to the caller. 
    // Example Validation Wrapper for AirTableService Responses. Currently we only have one type of resposne so we don't need a generic but its just for showcase. Also We could use a Custom wrapper structure, instead of a specific one so we don't need to define a new one for every case scenario.
    public class AirTableLogMessageResponse<T> where T : new()
    {
        public enum ErrorTypeEnum
        {
            ModelParseFail = 0,
            RequestFailToSend = 1,
            RemoteServerFailure = 2
        }

        private AirTableLogMessageResponse()
        {

        }

        internal static AirTableLogMessageResponse<T> CreateStatusResponseFailure(int statusCode, string message)
        {
            return new AirTableLogMessageResponse<T>()
            {
                Message = message,
                ResponseStatusCode = statusCode,
                IsSuccessfull = false,
                ErrorType = ErrorTypeEnum.RemoteServerFailure
            };
        }

        internal static AirTableLogMessageResponse<T> CreateSuccess(T model)
        {
            return new AirTableLogMessageResponse<T>()
            {
                ResponseModel = model,
                IsSuccessfull = true
            };
        }

        internal static AirTableLogMessageResponse<T> CreatJsonParseFailure(string message)
        {
            return new AirTableLogMessageResponse<T>()
            {
                ErrorType = ErrorTypeEnum.ModelParseFail,
                IsSuccessfull = false,
                Message = message
            };
        }
        internal static AirTableLogMessageResponse<T> CreatRequestSendFailure(string message)
        {
            return new AirTableLogMessageResponse<T>()
            {
                ErrorType = ErrorTypeEnum.RequestFailToSend,
                IsSuccessfull = false,
                Message = message
            };
        }

        private bool IsSuccessfull;

        public ErrorTypeEnum ErrorType;
        public bool IsSuccess => IsSuccessfull;
        public bool IsFailure => !IsSuccessfull;

        public string Message;

        public int? ResponseStatusCode;

        public T ResponseModel;
    }
}
