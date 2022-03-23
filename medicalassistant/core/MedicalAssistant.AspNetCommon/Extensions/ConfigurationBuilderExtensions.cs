using Microsoft.Extensions.Configuration;

namespace MedicalAssistant.AspNetCommon.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder SetupConfigurationSources(this IConfigurationBuilder configBuilder)
        {
            configBuilder.AddJsonFile("appsettings.json", false, true) // app defaults
                         .AddJsonFile("appsettings.Development.json", true, true) // development configuration
                         .AddJsonFile("appsettings.local.json", true, true) // local configuration
                         .AddEnvironmentVariables();

            return configBuilder;
        }
    }
}
