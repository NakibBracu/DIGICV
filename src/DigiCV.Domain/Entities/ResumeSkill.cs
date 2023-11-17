namespace DigiCV.Domain.Entities
{
    public class ResumeSkill
    {
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
        public Guid ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}