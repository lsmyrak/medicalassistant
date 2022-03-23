using Autofac.Extensions.DependencyInjection;
using CommandLine;
using MedicalAssistant.AspNetCommon.Extensions;
using MedicalAssistant.SurveyCovid.App.CommandLine;
using MedicalAssistant.SurveyCovid.Context;
using MedicalAssistant.SurveyCovid.SeedScenarios;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.App
{
    public abstract class Program
    {
        public static async Task<int> Main(string[] args)
        {
            try
            {
                using var host = CreateWebHost(args);
                using (var scope = host.Services.CreateScope())
                {
                    MigrateDatabase(scope);
                }

                await ActivatorUtilities.CreateInstance<InitialSeedScenario>(host.Services).Execute();

                return Parser.Default.ParseArguments<SeedOptions, StartOptions>(args)
                             .MapResult(
                                 HandleSeed(host),
                                 HandleStart(host),
                                 HandleParsingErrors);
            }
            catch (Exception ex)
            {
                Log.Error("Host terminated unexpectedly.");
                Log.Error(ex, ex.Message);
                return 1;
            }
        }

        private static Func<StartOptions, int> HandleStart(IWebHost host)
        {
            return options =>
            {
                Log.Information("Starting host...");
                host.Run();
                return 0;
            };
        }

        private static Func<SeedOptions, int> HandleSeed(IWebHost host)
        {
            return options =>
            {
                var scenarioType = GetSeedingScenarioType(options.SeedScenario);
                if (scenarioType == null)
                {
                    Console.Error.WriteLine("Unrecognized scenario name: {scenarioName}", options.SeedScenario);
                    return 1;
                }

                if (!(ActivatorUtilities.CreateInstance(host.Services, scenarioType) is ISeedScenario scenarioInstance))
                {
                    Log.Fatal("Failed creating instance of scenario: {scenarioName}", scenarioType.Name);
                    return 1;
                }

                Log.Information("Executing seed scenario: {scenarioName}", scenarioType.Name);
                scenarioInstance.Execute().Wait();
                Log.Information("Seeding finished.");
                return 0;
            };
        }

        private static int HandleParsingErrors(IEnumerable<Error> errors)
        {
            Log.Error("Could not parse launch arguments.");
            return 1;
        }

        private static void MigrateDatabase(IServiceScope scope)
        {
            scope.ServiceProvider.MigrateDatabase<SurveyCovidContext>(3);
        }

        private static IWebHost CreateWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
            .ConfigureServices(services => services.AddAutofac())
            .UseStartup<Startup>()
            .UseSerilog(
                 (hostingContext, loggerConfiguration) => loggerConfiguration
                     .SdLoggerConfiguration(new Uri(hostingContext.Configuration["SEQ_ADDRESS"], UriKind.Absolute), hostingContext.HostingEnvironment.IsDevelopment()))
            .Build();
        }

        private static Type GetSeedingScenarioType(string scenarioName)
        {
            return typeof(ISeedScenario)
            .Assembly
            .GetTypes()
            .FirstOrDefault(
            t => t.GetInterfaces().Contains(typeof(ISeedScenario)) &&
            t.Name.StartsWith(scenarioName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
