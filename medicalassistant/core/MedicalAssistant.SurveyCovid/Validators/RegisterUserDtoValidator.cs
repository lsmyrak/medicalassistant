using FluentValidation;
using MedicalAssistant.SurveyCovid.AccessData.Repositories;
using MedicalAssistant.SurveyCovid.Contracts.Dto;

namespace MedicalAssistant.SurveyCovid.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(IUserRepository userRepository)
        {
            RuleFor(x => x.Login).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6).Equal(x => x.ConfirmedPassword);
            RuleFor(x => x.RoleId).NotEmpty();
            RuleFor(x => x.Login).CustomAsync(async (value, context, _) =>
            {
                if (await userRepository.GetAsync(value) != null)
                {
                    context.AddFailure("Użytkownik istnieje w bazie");
                }
            });
        }
    }
}
