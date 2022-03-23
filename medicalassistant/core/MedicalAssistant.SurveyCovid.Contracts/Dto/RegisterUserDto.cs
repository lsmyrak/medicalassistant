using System;

namespace MedicalAssistant.SurveyCovid.Contracts.Dto
{
    public class RegisterUserDto
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string ConfirmedPassword { get; set; }

        public string Name { get; set; }

        public Guid RoleId { get; set; }

    }
}
