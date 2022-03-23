using System.Text;

namespace MedicalAssistant.SurveyCovid.Providers.PasswordHashProvider
{
    public class SaltedPasswordComponents
    {
        public SaltedPasswordComponents(string email, string password, string dbHashComponent, string configurationFileHashComponent)
        {
            Email = email;
            Password = password;
            DatabaseHashComponent = dbHashComponent;
            ConfigurationFileHashComponent = configurationFileHashComponent;
        }

        public string Email { get; }

        public string Password { get; }

        public string DatabaseHashComponent { get; }

        public string ConfigurationFileHashComponent { get; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(Email.Substring(0, 2));
            builder.Append(Password);
            builder.Append(Email.Substring(Email.Length - 2));
            builder.Append(DatabaseHashComponent);
            builder.Append(ConfigurationFileHashComponent);

            return builder.ToString();
        }

    }
}