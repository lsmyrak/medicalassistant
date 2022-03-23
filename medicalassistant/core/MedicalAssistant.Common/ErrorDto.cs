using System;

namespace MedicalAssistant.Common
{
    public class ErrorDto
    {
        public ErrorDto(string userMessage = null, string systemMessage = null, string stackTrace = null, string type = null)
        {
            CorrelationId = Guid.NewGuid();
            UserMessage = userMessage;
            SystemMessage = systemMessage;
            StackTrace = stackTrace;
            Type = type;
        }

        public Guid CorrelationId { get; }

        public string UserMessage { get; }

        public string SystemMessage { get; }

        public string StackTrace { get; }

        public string Type { get; }
    }
}
