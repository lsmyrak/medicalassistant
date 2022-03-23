

namespace MedicalAssistant.SurveyCovid.Providers.PasswordHashProvider
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool HasSameHashedPasswords(string saltedPasswordComponents, string retrievedUserPasswordHash);
    }
}
