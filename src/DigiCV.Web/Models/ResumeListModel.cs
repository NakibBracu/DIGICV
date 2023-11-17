using Autofac;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using DigiCV.Persistence.Features.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Formats.Asn1.AsnWriter;

namespace DigiCV.Web.Models
{
    public class ResumeListModel
    {
        private IResumeService _resumeService;
        private UserManager<ApplicationUser> _userManager;
        public ResumeListModel() { }
        public ResumeListModel(IResumeService resumeService, UserManager<ApplicationUser> userManager) 
        {
            _resumeService = resumeService;
            _userManager = userManager;
        }
        public void ResovleDependency(ILifetimeScope scope)
        {
            _resumeService = scope.Resolve<IResumeService>();
            _userManager = scope.Resolve<UserManager<ApplicationUser>>();
        }
        public async Task DeleteResume(Guid resumeId, Guid userId)
        {
            var resumesData = _resumeService.GetResumesByUserId(userId);
            if(resumesData != null)
            {
                if (resumesData.Select(x => x.Id).Any(x=>x==resumeId))
                {
                    await _resumeService.DeleteResume(resumeId);

                }
                else
                {
                    throw new Exception("Access Denied!");
                }
            }
            else
            {
                throw new Exception("Access Denied!");
            }
        }
        public IList<Resume> Resumes { get; set; }
        public async Task<IList<Resume>> GetResumes(ClaimsPrincipal user)
        {
            var appUser = await _userManager.GetUserAsync(user);
            return _resumeService.GetResumes().Result.Where(x => x.UserId == appUser.Id).ToList();
        }
    }
}
