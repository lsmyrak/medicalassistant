using System;


namespace MedicalAssistant.SurveyCovid.Providers.TokenProvider
{
    public interface ITokenProvider
    {
        (string, DateTime) GenerateToken(string tokenKey, string nameIdentifier, string role, string name);

        string GenerateRefreshToken();
    }

}
