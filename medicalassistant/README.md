# MedicalAssistant
## First time set-up, without Docker, for development and debugging:

### Prerequisites:

1. MS SQL Server 2019
2. .NET Core SDk 3.1 - https://dotnet.microsoft.com/download
3. NodeJs 12 and npm - https://nodejs.org/

### Steps:

1. Make sure that MS SQL is running
2. Generate SSL certificate for the App projects:

   ```bash
   cd .core/MedicalAssistant.SurveyCovid.App
   dotnet dev-certs https -ep "localhost.pfx" -p MPPassword12345 --trust
   ```


