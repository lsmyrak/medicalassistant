using AutoMapper;
using MedicalAssistant.SurveyCovid.AccessData.Repositories;
using MedicalAssistant.SurveyCovid.Configuration;
using MedicalAssistant.SurveyCovid.Contracts.Dto;
using MedicalAssistant.SurveyCovid.Entitis;
using MedicalAssistant.SurveyCovid.Providers.PasswordHashProvider;
using MedicalAssistant.SurveyCovid.Providers.TokenProvider;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IApplicationConfiguration _applicationConfiguration;
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;
        private readonly IRoleRepository _roleRepository;

        public AccountService(
            IMapper mapper,
            IPasswordHasher passwordHasher,
            IApplicationConfiguration applicationConfiguration,
            IUserRepository userRepository,
            ITokenProvider tokenProvider,
            IRoleRepository roleRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _applicationConfiguration = applicationConfiguration ?? throw new ArgumentNullException(nameof(applicationConfiguration));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task Register(RegisterUserDto registerUserDto)
        {
            var hashComponent = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
            var newSaltedPassword = new SaltedPasswordComponents(
                registerUserDto.Login,
                registerUserDto.Password,
                hashComponent,
                _applicationConfiguration.HashComponent);
            var user = _mapper.Map<User>(registerUserDto);
            user.SetHashComponent(hashComponent);
            user.SetHashPassword(_passwordHasher.HashPassword(newSaltedPassword.ToString()));
            await _userRepository.AddAsync(user);
        }

        public async Task<UserLoginResult> Login(UserLoginDto userLoginDto, string ipAddress)
        {
            var user = await _userRepository.GetAsync(userLoginDto.Login);
            if (user == null)
            {
                return UserLoginResult.Failure();
            }

            var saltedPassword = new SaltedPasswordComponents(
                userLoginDto.Login,
                userLoginDto.Password,
                user.HashComponent,
                _applicationConfiguration.HashComponent);
            if (_passwordHasher.HasSameHashedPasswords(saltedPassword.ToString(), user.HashPassword))
            {
                var (token, tokenExpires) = _tokenProvider.GenerateToken(
                        _applicationConfiguration.TokenKey,
                        user.Id.ToString(),
                        user.Role.Name,
                        user.Name);
                var refreshToken = CreateRefreshToken(ipAddress);

                user.AddRefreshToken(refreshToken);
                await _userRepository.UpdateAsync(user);

                return UserLoginResult.Success(
                    token,
                    tokenExpires,
                    user.Id,
                    user.Name,
                    refreshToken.Token,
                    refreshToken.Expires,
                    user.Role.Name
                    );
            }

            return UserLoginResult.Failure();
        }

        public async Task<UserLoginResult> RefreshToken(string token, string ipAddress)
        {
            var user = await _userRepository.GetForRefreshToken(token);

            // return null if no user found with token
            if (user == null) return null;

            var refreshToken = user.GetRefreshToken(token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return UserLoginResult.Failure();

            // replace old refresh token with a new one and save
            var newRefreshToken = new RefreshToken();

            newRefreshToken.SetToken(_tokenProvider.GenerateRefreshToken());
            newRefreshToken.SetExpires(DateTime.UtcNow.AddHours(2));
            newRefreshToken.SetCreated(DateTime.UtcNow);
            newRefreshToken.SetCreatedByIp(ipAddress);

            refreshToken.SetRevoked(DateTime.UtcNow);
            refreshToken.SetRevokedByIp(ipAddress);
            refreshToken.SetReplacedByToken(newRefreshToken.Token);
            user.RefreshTokens.Add(newRefreshToken);
            await _userRepository.UpdateAsync(user);

            var (newToken, newTokenExpires) = _tokenProvider.GenerateToken(
                _applicationConfiguration.TokenKey,
                user.Id.ToString(),
                user.Role.Name,
                user.Name);

            return UserLoginResult.Success(
                    newToken,
                    newTokenExpires,
                    user.Id,
                    user.Name,
                    newRefreshToken.Token,
                    newRefreshToken.Expires,
                    user.Role.Name
                    );
        }

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var user = await _userRepository.GetForRefreshToken(token);

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.GetRefreshToken(token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.SetRevoked(DateTime.UtcNow);
            refreshToken.SetRevokedByIp(ipAddress);
            await _userRepository.UpdateAsync(user);

            return true;
        }

        public async Task AddRoleAsync(Role role)
        {
            await _roleRepository.AddAsync(role);
        }

        private RefreshToken CreateRefreshToken(string ipAddress)
        {
            var refreshToken = new RefreshToken();
            refreshToken.SetToken(_tokenProvider.GenerateRefreshToken());
            refreshToken.SetExpires(DateTime.UtcNow.AddHours(2));
            refreshToken.SetCreated(DateTime.UtcNow);
            refreshToken.SetCreatedByIp(ipAddress);
            return refreshToken;
        }

    }
}
