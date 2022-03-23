using System;

namespace MedicalAssistant.SurveyCovid.Services.AccountService
{
    public class UserLoginResult
    {
        public bool Logged { get; set; }

        public string Token { get; set; }

        public DateTime TokenExpires { get; set; }

        public Guid? UserId { get; set; }

        public string Name { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpires { get; set; }

        public string Role { get; set; }

        public static UserLoginResult Success(string token, DateTime tokenExpires, Guid userId, string name, string refreshToken, DateTime refreshTokenExpires, string role)
        {
            return new UserLoginResult
            {
                Logged = true,
                Token = token,
                TokenExpires = tokenExpires,
                UserId = userId,
                Name = name,
                RefreshToken = refreshToken,
                RefreshTokenExpires = refreshTokenExpires,
                Role = role,
            };
        }

        public static UserLoginResult Failure()
        {
            return new UserLoginResult
            {
                Logged = false,
            };
        }
    }
}
