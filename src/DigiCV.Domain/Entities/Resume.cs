namespace DigiCV.Domain.Entities
{
    public class Resume : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ResumeTemplateId { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Skype { get; set; }
        public string LinkedIn { get; set; }
        public string Address { get; set; }
        public string Summary { get; set; }
        public string? ImageName { get; set; }
        public ICollection<string>? Trainings { get; set; } = new List<string>(); 
        public ResumeTemplate ResumeTemplate { get; set; }
        public ICollection<ResumeSkill>? Skills { get; set; } = new List<ResumeSkill>();
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Education> Educations { get; set; } = new List<Education>();
        public ICollection<Reference> References { get; set; } = new List<Reference>();
    }
}
