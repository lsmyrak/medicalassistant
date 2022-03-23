using System;

namespace MedicalAssistant.AspNetCommon.Exceptions
{
    public class CriticalException : Exception
    {
        protected CriticalException(string message)
            : base(message)
        {
        }
    }
}
