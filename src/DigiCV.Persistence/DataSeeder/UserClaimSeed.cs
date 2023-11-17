using DigiCV.Persistence.Features.Membership;

namespace DigiCV.Persistence.DataSeeder
{
    public static class UserClaimSeed
    {
        public static IList<ApplicationUserClaim> Claims()
        {
            // Adding Claim for ADMIN and MANAGER
            var claims = new List<ApplicationUserClaim>()
            {
                new ApplicationUserClaim()
                {
                    Id = 1,
                    UserId = new Guid("E26967F0-CE4C-4C14-8A0B-45BEB8C9EB48"),
                    ClaimType = "Administrator",
                    ClaimValue = "Administrator"
                },
                new ApplicationUserClaim()
                {
                    Id = 2,
                    UserId = new Guid("5F4C76D3-79B0-4923-86A7-511AC60C2AB9"),
                    ClaimType = "Manager",
                    ClaimValue = "Manager"
                }
            };

            return claims;
        }
    }
}
