using FluentValidation;
using MedicalAssistant.SurveyCovid.Contracts.Dto;

namespace MedicalAssistant.SurveyCovid.Validators
{
    class DepartmentDtoValidator : AbstractValidator<DepartmentDto>
    {
        public DepartmentDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Status).NotNull();
        }
    }
}
