using Autofac;
using AutoMapper;
using FluentValidation;
using MedicalAssistant.SurveyCovid.Configuration;
using MedicalAssistant.SurveyCovid.Contracts.Dto;
using MedicalAssistant.SurveyCovid.Mapping;
using MedicalAssistant.SurveyCovid.Providers.PasswordHashProvider;
using MedicalAssistant.SurveyCovid.Providers.TokenProvider;
using MedicalAssistant.SurveyCovid.Services;
using MedicalAssistant.SurveyCovid.Services.AccountService;
using MedicalAssistant.SurveyCovid.Validators;

namespace MedicalAssistant.SurveyCovid
{
    public class SurveyCovidModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<IConfigurationProvider>(ctx => new MapperConfiguration(cfg => cfg.AddMaps(typeof(SurveyCovidMapper)))).SingleInstance();
            builder.Register<IMapper>(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>(), ctx.Resolve)).InstancePerDependency();

            builder.RegisterType<ApplicationConfiguration>().As<IApplicationConfiguration>();
            builder.RegisterType<PasswordHasher>().As<IPasswordHasher>();
            builder.RegisterType<JwtTokenProvider>().As<ITokenProvider>();

            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<SurveyCovidService>().As<ISurveyCovidService>();
            builder.RegisterType<DepartmentService>().As<IDepartmentService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<RegisterUserDtoValidator>().As<IValidator<RegisterUserDto>>();
            builder.RegisterType<DepartmentDtoValidator>().As<IValidator<DepartmentDto>>();
            builder.RegisterType<SurveyReportService>().As<ISurveyReportService>();
        }
    }
}
