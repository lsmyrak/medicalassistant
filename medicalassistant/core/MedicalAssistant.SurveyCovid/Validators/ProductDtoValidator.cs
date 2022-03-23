using FluentValidation;
using MedicalAssistant.SurveyCovid.Contracts.Dto;

namespace MedicalAssistant.SurveyCovid.Validators
{
    class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.ProductCode).NotNull().NotEmpty();
            RuleFor(x => x.ProductName).NotNull().NotEmpty();
            RuleFor(x => x.UnitsCount).NotNull().NotEmpty();
            RuleFor(x => x.UnitValue).NotNull().NotEmpty();
        }

    }
}