using Autofac;
using MedicalAssistant.AspNetCommon.Logging;

namespace MedicalAssistant.AspNetCommon
{
    public class AspNetCommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Logger<>)).AsImplementedInterfaces();
        }
    }
}
