using DigiCV.Domain.Entities;
using DigiCV.Persistence.Features.Membership;
using Microsoft.AspNetCore.Identity;

namespace DigiCV.Persistence.DataSeeder
{
    public static class UserSeed
    {
        public static IList<ApplicationUser> Users()
        {
            var passHasher = new PasswordHasher<ApplicationUser>();

            // Creating ADMIN account
            var admin = new ApplicationUser()
            {
                Id = new Guid("E26967F0-CE4C-4C14-8A0B-45BEB8C9EB48"),
                FullName = "Admin",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@digicv.com",
                NormalizedEmail = "ADMIN@DIGICV.COM",
                EmailConfirmed = true,
		        JoiningDate = DateTime.Now,
                PhoneNumber = "+8801856817465",
                SecurityStamp = "BFCC7B453A8B4B6C8A4C93EE28A3B4A8",
                LockoutEnabled = true,
                UserProfileId = new Guid("9749E4E1-F89C-42BD-8A04-5A79993A58F9")
            };

            admin.PasswordHash = passHasher.HashPassword(admin, "123456");

            // Creating MANAGER account
            var manager = new ApplicationUser()
            {
                Id = new Guid("5F4C76D3-79B0-4923-86A7-511AC60C2AB9"),
                FullName = "Manager",
                UserName = "manager",
                NormalizedUserName = "MANAGER",
                Email = "manager@digicv.com",
                NormalizedEmail = "MANAGER@DIGICV.COM",
                EmailConfirmed = true,
		        JoiningDate = DateTime.Now,
                PhoneNumber = "+8801856817465",
                SecurityStamp = "FC37C84E276C4D978DF9054129D0CB23",
                LockoutEnabled = true,
                UserProfileId = new Guid("A618E70B-3CDB-420A-A1B3-702D0B06E9C2")
            };
            
            manager.PasswordHash = passHasher.HashPassword(manager, "123456");

            var users = new List<ApplicationUser>() { admin, manager };

            return users;
        }
    }
}
