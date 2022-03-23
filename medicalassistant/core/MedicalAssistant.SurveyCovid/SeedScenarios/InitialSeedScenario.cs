using MedicalAssistant.SurveyCovid.AccessData.Repositories;
using MedicalAssistant.SurveyCovid.Contracts.Dto;
using MedicalAssistant.SurveyCovid.Entitis;
using MedicalAssistant.SurveyCovid.Services;
using System;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.SeedScenarios
{
    public class InitialSeedScenario : ISeedScenario
    {
        private const string SeedCompletedTimestampKey = "SeedCompletedTimestamp";
        private readonly IAccountService _accountService;
        private readonly ISettingsRepository _settingsRepository;

        public InitialSeedScenario(
            IAccountService accountService,
            ISettingsRepository settingsRepository)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _settingsRepository = settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));
        }

        public async Task Execute()
        {
            if (!await _settingsRepository.KeyExistsAsync(SeedCompletedTimestampKey))
            {
                await SeedRolesAndUsers();

                await _settingsRepository.AddValueAsync(
                    SeedCompletedTimestampKey,
                    DateTimeOffset.UtcNow,
                    "Date of last completed DB seed. Delete this row to trigger a reseed on app restart.");
            }
        }

        private async Task SeedRolesAndUsers()
        {
            await _accountService.AddRoleAsync(new Role(SeedConstants.SeedAdminRoleId, "admin"));
            await _accountService.AddRoleAsync(new Role(SeedConstants.SeedUserRoleId, "user"));

            await _accountService.Register(new RegisterUserDto
            {
                Login = "admin@test.pl",
                Password = "123456",
                ConfirmedPassword = "123456",
                Name = "Administrator",
                RoleId = SeedConstants.SeedAdminRoleId
            });

            await _accountService.Register(new RegisterUserDto
            {
                Login = "user@test.pl",
                Password = "123456",
                ConfirmedPassword = "123456",
                Name = "Użytkownik",
                RoleId = SeedConstants.SeedUserRoleId
            });

        }
    }
}
