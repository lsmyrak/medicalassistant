using Serilog;
using Serilog.Events;
using System;

namespace MedicalAssistant.AspNetCommon.Extensions
{
    public static class SerilogExtensions
    {
        public static LoggerConfiguration SdLoggerConfiguration(
            this LoggerConfiguration loggerConfiguration,
            Uri seqServiceUrl,
            bool isDevelopment)
        {
            if (isDevelopment)
            {
                loggerConfiguration
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .MinimumLevel.Override("System", LogEventLevel.Information);
            }
            else
            {
                loggerConfiguration
                    .MinimumLevel.Warning()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Warning);
            }

            return loggerConfiguration
                   .Enrich.FromLogContext()
                   .Enrich.WithProcessId()
                   .Enrich.WithThreadId()
                   .Enrich.WithAssemblyName()
                   .Enrich.WithAssemblyVersion()
                   .Enrich.WithMachineName()
                   .WriteTo.Console(
                       outputTemplate:
                       "[{Timestamp:HH:mm:ss}] [{LineNumber}] [{Level:u3}] => [{AssemblyName}(v-{AssemblyVersion})] [{SourceContext}] [{MemberName}] -> [{ProcessId}/{ThreadId}]) => {Message}{NewLine}{Exception}")
                   .WriteTo.Seq(seqServiceUrl.ToString(), apiKey: "none");
        }

        public static ILogger SetContextForCallerAttribute(
            this ILogger log,
            string memberName,
            int sourceLineNumber)
        {
            return log.ForContext("MemberName", memberName)
.ForContext("LineNumber", sourceLineNumber);
        }
    }
}
