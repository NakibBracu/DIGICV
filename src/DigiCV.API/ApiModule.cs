using Autofac;
using DigiCV.API.Models;

namespace DigiCV.API
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContactModel>().AsSelf();
            
            base.Load(builder);
        }
    }
}
