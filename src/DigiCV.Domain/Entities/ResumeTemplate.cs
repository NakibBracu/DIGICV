using System.ComponentModel.DataAnnotations;

namespace DigiCV.Domain.Entities;

public class ResumeTemplate:IEntity<Guid>
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? ImageName { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Resume>? Resumes { get; set; } = new List<Resume>();
}