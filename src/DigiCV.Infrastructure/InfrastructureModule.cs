using Autofac;
using DigiCV.Application.Features.Training.Repositories;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Services;
using DigiCV.Infrastructure.Features.Services;
using DigiCV.Infrastructure.Securities;

namespace DigiCV.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ResumeService>().As<IResumeService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<SkillService>().As<ISkillService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<HtmlEmailService>().As<IEmailService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<EmailMessageService>().As<IEmailMessageService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<TemplateService>().As<ITemplateService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ContactService>().As<IContactService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<TokenService>().As<ITokenService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CoverLetterService>().As<ICoverLetterService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}