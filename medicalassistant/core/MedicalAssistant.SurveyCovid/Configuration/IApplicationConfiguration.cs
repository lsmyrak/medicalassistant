namespace MedicalAssistant.SurveyCovid.Configuration
{
    public interface IApplicationConfiguration
    {
        string HashComponent { get; }
        string TokenKey { get; }

    }
}
