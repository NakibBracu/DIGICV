using Autofac;
using Autofac.Core;
using DigiCV.Web.Areas.Admin.Models;
using DigiCV.Web.Models;
using DigiCV.Web.Models.Builder;
using DigiCV.Web.Models.Letter;
using DigiCV.Web.Models.PDF;
using DigiCV.Web.Service;

namespace DigiCV.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BuilderCreateModel>().AsSelf()
                .InstancePerLifetimeScope();
            builder.RegisterType<BuilderUpdateModel>().AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<RegisterModel>().AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<LoginModel>().AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserListModel>().AsSelf()
                .InstancePerLifetimeScope();
            builder.RegisterType<FileService>().As<IFileService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ResumeViewModel>().AsSelf().
            InstancePerLifetimeScope();

            builder.RegisterType<TemplateListModel>().AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<PdfGenerationHelper>().As<IPdfGenerationHelper>()
             .InstancePerLifetimeScope();
            builder.RegisterType<ViewRenderer>().As<IViewRenderer>().InstancePerLifetimeScope();

            builder.RegisterType<SkillCreateModel>().AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<SkillListModel>().AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<SkillUpdateModel>().AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<TemplateCreateModel>().AsSelf()
               .InstancePerLifetimeScope();

            builder.RegisterType<TemplateUpdateModel>().AsSelf()
               .InstancePerLifetimeScope();

            builder.RegisterType<ContactListModel>().AsSelf()
               .InstancePerLifetimeScope();

            builder.RegisterType<UserProfileModel>().AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserUpdateModel>().AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<ContactModel>().AsSelf()
                .InstancePerLifetimeScope();
                
            builder.RegisterType<ResumeListModel>().AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<LetterCreateModel>().AsSelf()
                .InstancePerLifetimeScope();
            builder.RegisterType<LetterUpdateModel>().AsSelf()
                .InstancePerLifetimeScope();
            builder.RegisterType<LetterViewModel>().AsSelf()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}