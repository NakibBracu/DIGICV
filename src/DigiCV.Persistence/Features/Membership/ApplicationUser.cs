using DigiCV.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DigiCV.Persistence.Features.Membership
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		public string? FullName { get; set; }
		public DateTime JoiningDate { get; set; }
		public Guid UserProfileId { get; set; }
		public UserProfile UserProfile { get; set; }
        public ICollection<Resume>? Resumes { get; set; }
        public ICollection<CoverLetter>? Letters { get; set; }
    }
}
