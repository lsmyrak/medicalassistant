using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MedicalAssistant.SurveyCovid.Providers.PasswordHashProvider
{
    public class PasswordHasher : IPasswordHasher
    {
        public bool HasSameHashedPasswords(string saltedPasswordComponents, string retrievedUserPasswordHash)
        {
            return HashPassword(saltedPasswordComponents) == retrievedUserPasswordHash;
        }

        public string HashPassword(string password)
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(password));
            using var hashProvider = new SHA256Managed();
            var hash = hashProvider.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", string.Empty, StringComparison.InvariantCulture);
        }

    }
}