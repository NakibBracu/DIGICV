namespace DigiCV.Domain.Entities
{
    public class Skill:IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ResumeSkill>? Resumes { get; set; } = new List<ResumeSkill>();
    }
}