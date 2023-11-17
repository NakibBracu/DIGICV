using Autofac;
using AutoMapper;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using DigiCV.Infrastructure.Features.Services;
using DigiCV.Persistence.Features.Membership;
using DigiCV.Web.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace DigiCV.Web.Models
{
    public class UserProfileModel
    {
        private IFileService _fileService;
        private UserManager<ApplicationUser> _userManager;
        private IUserService _userService;
        private IMapper _mapper;
        private IResumeService _resumeService;
        private ICoverLetterService _coverLetterService;
        public string ProfileImageFolder = Path.Combine("images", "profileImages");
        public UserProfileModel()
        {

        }
        public UserProfileModel(IFileService fileService,
            UserManager<ApplicationUser> userManager,
            IUserService userService,
            IMapper mapper,
            IResumeService resumeService, 
            ICoverLetterService coverLetterService
            )
        {
            _fileService = fileService;
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
            _resumeService = resumeService;
            _coverLetterService = coverLetterService;
        }
        [Required]
        [MinLength(3, ErrorMessage = "Name should Cotains atleast 3 character")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime JoiningDate { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile? UserProfile { get; set; } = new UserProfile();
        public IList<(Guid Id, string Title)>? Resumes { get; set; }
        public IList<(Guid Id, string Title)>? CoverLetters { get; set; }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _fileService = scope.Resolve<IFileService>();
            _userManager = scope.Resolve<UserManager<ApplicationUser>>();
            _userService = scope.Resolve<IUserService>();
            _mapper = scope.Resolve<IMapper>();
            _resumeService = scope.Resolve<IResumeService>();
            _coverLetterService = scope.Resolve<ICoverLetterService>();
        }
        public void LoadResumeIdandTitleList(Guid userId)
        {
            Resumes = _resumeService.GetResumesByUserId(userId);
        }
        public void LoadCoverLetterIdandTitleList(Guid userId)
        {
            CoverLetters = _coverLetterService.GetCoverLettersByUserId(userId);
        }
        public async Task LoadData(ClaimsPrincipal user)
        {
            var appUser = await _userManager.GetUserAsync(user);
            if (appUser != null)
            {
                LoadResumeIdandTitleList(appUser.Id);
                LoadCoverLetterIdandTitleList(appUser.Id);
                UserName = appUser.UserName;
                FullName = appUser.FullName!;
                Email = appUser.Email!;
                PhoneNumber = appUser.PhoneNumber;
                UserProfileId = appUser.UserProfileId;
                JoiningDate = appUser.JoiningDate;
                _mapper.Map(appUser.UserProfile, UserProfile);
            }
        }
        public async Task UpdateProfileImage(ClaimsPrincipal user, IFormFile file)
        {
            var appUser = await _userManager.GetUserAsync(user);
            if (appUser != null)
            {
                string previousImageName = appUser.UserProfile.ImageUrl;
                appUser.UserProfile.ImageUrl = _fileService.SaveFile(file, ProfileImageFolder);
                await _userManager.UpdateAsync(appUser);
                if(previousImageName != null)
                {
                    _fileService.DeleteFile(Path.Combine(ProfileImageFolder, previousImageName));
                }
            }
        }
        public async Task UpdateProfile(ClaimsPrincipal user)
        {
            var appUser = await _userManager.GetUserAsync(user);
            if(UserProfile.LinkedInUsername != null)
            {
                UserProfile.LinkedInUsername=UserProfile.LinkedInUsername.Trim('/');
            }
            if(UserProfile.GithubUsername != null)
            {
                UserProfile.GithubUsername=UserProfile.GithubUsername.Trim('/');
                
            }
            if (appUser != null)
            {
                appUser.FullName = FullName;
                appUser.PhoneNumber = PhoneNumber;
                var existingProfile = new UserProfile();
                _mapper.Map(appUser.UserProfile, existingProfile);
                _mapper.Map(UserProfile, appUser.UserProfile);
                appUser.UserProfile.Id = existingProfile.Id;
                appUser.UserProfile.ImageUrl = existingProfile.ImageUrl;
                await _userManager.UpdateAsync(appUser);
            }
        }
    }
}
