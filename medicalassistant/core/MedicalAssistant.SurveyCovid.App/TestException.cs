using MedicalAssistant.Common.Exceptions;

namespace MedicalAssistant.SurveyCovid.App
{
    public class TestException : GenericException
    {
        public TestException(string userMessage)
            : base(userMessage, httpStatusCode: System.Net.HttpStatusCode.BadRequest)
        {
        }
    }
}
