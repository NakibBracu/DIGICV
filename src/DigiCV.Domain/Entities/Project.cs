namespace DigiCV.Domain.Entities
{
    public class Project : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public Guid ResumeId { get; set; }
        public Resume? Resume { get; set; }
    }
}



