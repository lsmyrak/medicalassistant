using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MedicalAssistant.AspNetCommon.Logging
{
    public interface ILogger<T>
    {
        void Debug(
            string message,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0);

        void Debug(
            string message,
            Exception ex,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0);

        void Error(
            Exception ex,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0);

        void Error(
            string message,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0);

        void Error(
            string message,
            Exception ex,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0);

        void Info(
            string message,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0);

        void InfoLine(
            string message,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0);

        void Info(
            (string, object[]) logTemplate,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0);

        void Info(
            string message,
            Exception ex,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0);

        void Warning(
            string message,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0);

        void Warning(
            string message,
            Exception ex,
            [CallerMemberName]
            string memberName = "",
            [CallerLineNumber]
            int sourceLineNumber = 0);

        void RegisterApiCall(
            string email,
            string userId,
            string parameters,
            string message,
            IEnumerable<string> modelErrors,
            IDictionary<string, object> actionArguments);
    }
}