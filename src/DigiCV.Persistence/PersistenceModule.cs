using Autofac;
using DigiCV.Application;
using DigiCV.Application.Features.Training.Repositories;
using DigiCV.Domain.Utilities;
using DigiCV.Persistence.Training.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DigiCV.Persistence
{
    public class PersistenceModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        public PersistenceModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }
        protected override void Load(ContainerBuilder builder)
        { 
            builder.RegisterType<ResumeRepository>().As<IResumeRepository>()
                 .InstancePerLifetimeScope();

            builder.RegisterType<SkillRepository>().As<ISkillRepository>()
               .InstancePerLifetimeScope();
            builder.RegisterType<EmailRepository>().As<IEmailRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.Register(x => new AdoNetUtility(
                x.Resolve<ApplicationDbContext>().Database.GetDbConnection(), 300))
                .As<IAdoNetUtility>().InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TemplateRepository>().As<ITemplateRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ResumeTemplateRepository>().As<IResumeTemplateRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ContactRepository>().As<IContactRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CoverLetterRepository>().As<ICoverLetterRepository>()
                .InstancePerLifetimeScope();


            builder.RegisterType<UserProfileRepository>().As<IUserProfileRepository>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}