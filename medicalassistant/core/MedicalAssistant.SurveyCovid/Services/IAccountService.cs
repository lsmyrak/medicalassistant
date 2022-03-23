using MedicalAssistant.SurveyCovid.Contracts.Dto;
using MedicalAssistant.SurveyCovid.Entitis;
using MedicalAssistant.SurveyCovid.Services.AccountService;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.Services
{
    public interface IAccountService
    {
        Task Register(RegisterUserDto registerUserDto);

        Task<UserLoginResult> Login(UserLoginDto userLoginDto, string ipAddress);

        Task<UserLoginResult> RefreshToken(string token, string ipAddress);

        Task<bool> RevokeToken(string token, string ipAddress);

        Task AddRoleAsync(Role role);

    }
}
