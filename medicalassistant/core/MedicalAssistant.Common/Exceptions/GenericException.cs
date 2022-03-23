using System;
using System.Net;

namespace MedicalAssistant.Common.Exceptions
{
    public class GenericException : Exception
    {
        public GenericException(string userMessage = null, string systemMessage = null, Exception innerException = null, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
        : base(systemMessage, innerException)
        {
            UserMessage = userMessage;
            HttpStatusCode = httpStatusCode;
        }

        public string UserMessage { get; }

        public HttpStatusCode HttpStatusCode { get; }
    }
}
