using FluentValidation;
using MedicalAssistant.SurveyCovid.Contracts.Dto;

namespace MedicalAssistant.SurveyCovid.Validators
{
    class SurveyDtoValidator : AbstractValidator<SurveyDto>
    {
        public SurveyDtoValidator()
        {
            RuleFor(x => x.FromDate).NotEmpty().NotNull();
            RuleFor(x => x.UntilDate).NotEmpty().NotNull();
            RuleFor(x => x.Place).NotEmpty().NotNull();
        }
    }
}
