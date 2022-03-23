using MedicalAssistant.AspNetCommon.Middeleware;
using Microsoft.AspNetCore.Builder;

namespace MedicalAssistant.AspNetCommon.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
