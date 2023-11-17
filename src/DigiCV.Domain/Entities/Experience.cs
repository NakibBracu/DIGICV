namespace DigiCV.Domain.Entities
{
    public class Experience : IEntity<int>
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public string Companay { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime ResignationDate { get; set; }
        public ICollection<string>? Responsibilities { get; set; } = new List<string>();
        public Guid? ResumeId { get; set; }
        public Resume? Resume { get; set; }
    }
}
