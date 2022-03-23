using CommandLine;

namespace MedicalAssistant.SurveyCovid.App.CommandLine
{
    [Verb("seed", HelpText = "Seed the database using mock data.")]
    public class SeedOptions
    {
        public SeedOptions(string seedScenario)
        {
            SeedScenario = seedScenario;
        }

        [Value(0, Required = true, HelpText = "Name of the scenario. Uses 'StartsWith' to match the class name.")]
        public string SeedScenario { get; }
    }
}
