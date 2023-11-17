using DigiCV.Domain.Entities;
using DigiCV.Persistence.DataSeeder;
using DigiCV.Persistence.Features.Membership;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace DigiCV.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole,
        ApplicationUserLogin, ApplicationRoleClaim,
        ApplicationUserToken>,
        IApplicationDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public ApplicationDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    (x) => x.MigrationsAssembly(_migrationAssembly));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding data for ADMIN and MANAGER with their CLAIMS
            modelBuilder.Entity<UserProfile>().HasData(UserProfileSeed.UserProfiles());
            modelBuilder.Entity<ApplicationUser>().HasData(UserSeed.Users());
            modelBuilder.Entity<ApplicationUserClaim>().HasData(UserClaimSeed.Claims());
            modelBuilder.Entity<Skill>().HasData(SkillSeed.SkillList());
            modelBuilder.Entity<ResumeTemplate>().HasData(ResumeTemplateSeed.ResumeTemplateList());

            modelBuilder.Entity<ResumeSkill>()
                .HasKey(x => new { x.ResumeId, x.SkillId });

            modelBuilder.Entity<ResumeSkill>()
                .HasOne(x => x.Skill)
                .WithMany(x => x.Resumes)
                .HasForeignKey(x => x.SkillId);

            modelBuilder.Entity<ResumeSkill>()
                .HasOne(x => x.Resume)
                .WithMany(x => x.Skills)
                .HasForeignKey(x => x.ResumeId);

            modelBuilder.Entity<Resume>().HasMany(x => x.Educations)
                .WithOne(c => c.Resume)
                .HasForeignKey(f => f.ResumeId);

            modelBuilder.Entity<Resume>().HasMany(x => x.Experiences)
                .WithOne(c => c.Resume)
                .HasForeignKey(f => f.ResumeId);

            modelBuilder.Entity<Resume>().HasMany(x => x.Projects)
                .WithOne(c => c.Resume)
                .HasForeignKey(f => f.ResumeId);

            modelBuilder.Entity<Resume>().HasMany(x => x.References)
                .WithOne(c => c.Resume)
                .HasForeignKey(f => f.ResumeId);

            modelBuilder.Entity<ApplicationUser>()
                .Navigation(p => p.UserProfile)
                .AutoInclude();

            modelBuilder.Entity<ApplicationUser>().
                HasMany(x => x.Resumes).WithOne().HasForeignKey(x => x.UserId).IsRequired();
            modelBuilder.Entity<ApplicationUser>().
                HasMany(x => x.Letters).WithOne().HasForeignKey(x => x.UserId).IsRequired();

            modelBuilder.Entity<Resume>().Property(x => x.Trainings)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                    v => JsonSerializer.Deserialize<ICollection<string>>(v, JsonSerializerOptions.Default))
                    .Metadata.SetValueComparer(new ValueComparer<ICollection<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));

            modelBuilder.Entity<Experience>().Property(x => x.Responsibilities)
                .HasConversion(
                  v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                  v => JsonSerializer.Deserialize<ICollection<string>>(v, JsonSerializerOptions.Default))
                  .Metadata.SetValueComparer(new ValueComparer<ICollection<string>>(
                  (c1, c2) => c1.SequenceEqual(c2),
                  c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                  c => c.ToList()));

            modelBuilder.Entity<Resume>().Navigation(c => c.Educations).AutoInclude();
            modelBuilder.Entity<Resume>().Navigation(c => c.Skills).AutoInclude();
            modelBuilder.Entity<Resume>().Navigation(c => c.Experiences).AutoInclude();
            modelBuilder.Entity<Resume>().Navigation(c => c.Projects).AutoInclude();
            modelBuilder.Entity<Resume>().Navigation(c => c.References).AutoInclude();
            modelBuilder.Entity<ResumeSkill>().Navigation(c => c.Skill).AutoInclude();
            modelBuilder.Entity<Template>();
            modelBuilder.Entity<Contact>();

        }
        public DbSet<Email> Emails { get; set; }
        public DbSet<CoverLetter> CoverLetters { get; set; }
    }
}
