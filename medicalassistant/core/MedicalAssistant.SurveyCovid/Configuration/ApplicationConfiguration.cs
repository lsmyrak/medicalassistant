using Microsoft.Extensions.Configuration;
using System;




namespace MedicalAssistant.SurveyCovid.Configuration
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        private const string HashComponentKey = "HASH_COMPONENT";
        private const string TokenKeyKey = "TOKEN_KEY";

        public ApplicationConfiguration(IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            HashComponent = configuration[HashComponentKey];
            TokenKey = configuration[TokenKeyKey];

            ValidateConfiguration();
        }

        public string HashComponent { get; }

        public string TokenKey { get; }

        private void ValidateConfiguration()
        {
            if (string.IsNullOrEmpty(HashComponent))
            {
                throw new Exception(message: $"{nameof(HashComponent)}, Hash Component cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(TokenKey))
            {
                throw new Exception(message: $"{nameof(HashComponent)}, Token Key cannot be null or empty.");
            }
        }

    }
}