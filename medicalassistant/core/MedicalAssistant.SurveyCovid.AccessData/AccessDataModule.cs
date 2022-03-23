using Autofac;
using MedicalAssistant.SurveyCovid.AccessData.Repositories;

namespace MedicalAssistant.SurveyCovid.AccessData
{
    public class AccessDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DepartmentRepository>().AsImplementedInterfaces();
            builder.RegisterType<ProductRepository>().AsImplementedInterfaces();
            builder.RegisterType<RoleRepository>().AsImplementedInterfaces();
            builder.RegisterType<SurveyRepository>().AsImplementedInterfaces();
            builder.RegisterType<UserRepository>().AsImplementedInterfaces();
            builder.RegisterType<SettingsRepository>().AsImplementedInterfaces();
            builder.RegisterType<DepartmentRepository>().AsImplementedInterfaces();
        }
    }
}
