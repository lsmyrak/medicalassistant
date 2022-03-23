using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.SeedScenarios
{
    public interface ISeedScenario
    {
        public Task Execute();
    }
}
