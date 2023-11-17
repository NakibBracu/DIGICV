using Microsoft.AspNetCore.Identity;
using System;

namespace DigiCV.Persistence.Features.Membership
{
	public class ApplicationRole : IdentityRole<Guid>
	{
		public ApplicationRole()
			: base()
		{
		}
		
		public ApplicationRole(string roleName)
			: base(roleName)
		{
		}
	}
}
