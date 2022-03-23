using System;

namespace MedicalAssistant.SurveyCovid.Contracts.Dto
{
    public class UserLoginResultDto
    {
        public string Token { get; set; }

        public DateTime TokenExpires { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpires { get; set; }

        public string Role { get; set; }

    }
}
