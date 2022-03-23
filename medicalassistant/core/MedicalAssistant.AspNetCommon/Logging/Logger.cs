using MedicalAssistant.AspNetCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MedicalAssistant.AspNetCommon.Logging
{
    public class Logger<T> : ILogger<T>
        where T : class
    {
        private readonly Serilog.ILogger _log = Serilog.Log.ForContext<T>();

        public void Debug(
            string message,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0)
        {
            _log.SetContextForCallerAttribute(memberName, sourceLineNumber).Debug(message);
        }

        public void Debug(
            string message,
            Exception ex,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0)
        {
            _log.SetContextForCallerAttribute(memberName, sourceLineNumber).Debug(FormatExceptionAndMessage(message, ex));
        }

        public void Info(
            string message,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0)
        {
            _log.SetContextForCallerAttribute(memberName, sourceLineNumber).Information(message);
        }

        public void InfoLine(
            string message,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0)
        {
            _log.SetContextForCallerAttribute(memberName, sourceLineNumber).Information(Environment.NewLine + message);
        }

        public void Info(
            (string, object[]) logTemplate,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0)
        {
            _log.SetContextForCallerAttribute(memberName, sourceLineNumber).Information(logTemplate.Item1, logTemplate.Item2);
        }

        public void Info(
            string message,
            Exception ex,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0)
        {
            _log.SetContextForCallerAttribute(memberName, sourceLineNumber).Information(FormatExceptionAndMessage(message, ex));
        }

        public void Warning(
            string message,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0)
        {
            _log.SetContextForCallerAttribute(memberName, sourceLineNumber).Warning(message);
        }

        public void Warning(
            string message,
            Exception ex,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0)
        {
            _log.SetContextForCallerAttribute(memberName, sourceLineNumber).Warning(FormatExceptionAndMessage(message, ex));
        }

        public void Error(
            string message,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0)
        {
            _log.SetContextForCallerAttribute(memberName, sourceLineNumber).Error(message);
        }

        public void Error(
            string message,
            Exception ex,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0)
        {
            _log.SetContextForCallerAttribute(memberName, sourceLineNumber).Error(FormatExceptionAndMessage(message, ex));
        }

        public void Error(
            Exception ex,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0)
        {
            _log.SetContextForCallerAttribute(memberName, sourceLineNumber).Error(ex.ToString());
        }

        public void RegisterApiCall(string email, string userId, string parameters, string message, IEnumerable<string> errors, IDictionary<string, object> actionArguments)
        {
            _log
                .ForContext("EventType", "ApiCallActivity")
                .ForContext("Email", email)
                .ForContext("UserId", userId)
                .ForContext("Parameters", parameters)
                .ForContext("Errors", errors.ToArray())
                .ForContext("ActionArguments", actionArguments, true)
                .Information(message);
        }

        private static string FormatExceptionAndMessage(string message, Exception ex) =>
            $"{message}: {(ex != null ? ex.ToString() : string.Empty)}";
    }
}
