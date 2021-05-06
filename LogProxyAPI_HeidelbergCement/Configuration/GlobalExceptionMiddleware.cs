using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LogProxyAPI_HeidelbergCement.Configuration
{
    // Globbal middleware for Handling exceptions
    public class GlobalExceptionMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILoggerFactory factory)
        {
            _logger = factory.CreateLogger("ExceptionLogger");
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Consistent Error Result Model. Its dynamic just for showcase.
            // We could Handle different type of Exceptions. For example BussinessExceptions, that cotain some validation result data and map it to the Error Result Model.
            var errorResult = new
            {
                Message = exception.Message,
                Code = 500
            };

            var serializedError = JsonSerializer.Serialize(errorResult);

            _logger.LogError(serializedError);

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(serializedError);
        }
    } 
}
