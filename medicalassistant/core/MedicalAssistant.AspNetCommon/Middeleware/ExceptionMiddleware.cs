using MedicalAssistant.Common;
using MedicalAssistant.Common.Exceptions;
using MedicalAssistant.Common.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MedicalAssistant.AspNetCommon.Middeleware
{
    public class ExceptionMiddleware
    {
        private const string DefaultExceptionMessage = "Unknown system error. Please contact our administrator.";
        private const string ApplicationJson = "application/json";

        private readonly JsonSerializer _serializer;
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        //private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _env = env ?? throw new ArgumentNullException(nameof(env));
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serializer = new JsonSerializer { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        }

        protected bool IsDevelopmentEnvironment => _env.IsDevelopment();

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static int GetStatusCode(Exception exception)
        {
            return exception is GenericException genericException
? (int)genericException.HttpStatusCode
: (int)HttpStatusCode.InternalServerError;
        }

        private static string GetUserMessage(Exception exception)
        {
            return exception is GenericException genericException
? genericException.UserMessage
: DefaultExceptionMessage;
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = ApplicationJson;
            context.Response.StatusCode = GetStatusCode(exception);

            var userMessage = GetUserMessage(exception);
            var systemMessage = GetSystemMessage(exception);
            var stackTrace = GetStackTrace(exception);
            var exceptionType = GetErrorType(exception);

            var errorDto = new ErrorDto(userMessage, systemMessage, stackTrace, exceptionType);
            var responseBody = _serializer.SerializeObject(errorDto);

            return context.Response.WriteAsync(responseBody);
        }

        private string GetErrorType(Exception exception)
        {
            return IsDevelopmentEnvironment ? exception.GetType().Name : string.Empty;
        }

        private string GetStackTrace(Exception exception)
        {
            return IsDevelopmentEnvironment ? exception.StackTrace : string.Empty;
        }

        private string GetSystemMessage(Exception exception)
        {
            return IsDevelopmentEnvironment ? exception.Message : string.Empty;
        }
    }
}
