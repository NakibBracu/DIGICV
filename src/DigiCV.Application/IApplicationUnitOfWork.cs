using DigiCV.Application.Features.Training.Repositories;
using DigiCV.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Application
{
    public interface IApplicationUnitOfWork: IUnitOfWork
    {
        // No need to redeclare or implement SaveAsync() here. Because the child interface automatically inherits the method signatures (i.e., the method names, parameter lists, and return types) from the parent interface. This the flexibility of Code Reusibility.
        IEmailRepository Emails { get; }
        IResumeRepository Resumes { get;}
        ISkillRepository Skills { get;}
        IResumeTemplateRepository ResumeTemplates { get;}
        IContactRepository Contacts { get; }
        ICoverLetterRepository CoverLetters { get; }
        IUserProfileRepository UserProfiles { get; }

    }
}
