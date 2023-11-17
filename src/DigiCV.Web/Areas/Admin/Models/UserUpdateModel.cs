using Autofac;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using DigiCV.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DigiCV.Web.Areas.Admin.Models
{
    public class UserUpdateModel
    {
        private IUserService _userService;
        public UserUpdateModel() { }
        public UserUpdateModel(IUserService userService)
        {
            _userService = userService;
        }

        [Required]
        public Guid Id { get; set; }
        public string? Address { get; set; }
        public string? Education { get; set; }
        public string? Experience { get; set; }
        public string? ImageUrl { get; set; }
        public string? Designation { get; set; }
        public string? GithubUsername { get; set; }
        public string? LinkedInUsername { get; set; }
        public bool IsActive { get; set; }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _userService = scope.Resolve<IUserService>();
        }

        internal void Load(Guid id)
        {
            UserProfile userProfile = _userService.GetUser(id);
            Address = userProfile.Address;
            Education = userProfile.Education;
            Experience = userProfile.Experience;
            ImageUrl = userProfile.ImageUrl;
            Designation = userProfile.Designation;
            GithubUsername = userProfile.GithubUsername;
            LinkedInUsername = userProfile.LinkedInUsername;
            IsActive = userProfile.IsActive;
        }

        internal void Update()
        {
            _userService.UpdateUser(Id, Address, Education, Experience, ImageUrl, Designation, GithubUsername, LinkedInUsername, IsActive);
        }
    }
}