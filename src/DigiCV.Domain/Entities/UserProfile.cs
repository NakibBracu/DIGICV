namespace DigiCV.Domain.Entities
{
    public class UserProfile : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string? Address { get; set; }
        public string? Education { get; set; }
        public string? Experience { get; set; }
        public string? ImageUrl { get; set; }
        public string? Designation { get; set; }
        public string? GithubUsername { get; set; }
        public string? LinkedInUsername { get; set; }
        public bool IsActive { get; set; }
    }
}
