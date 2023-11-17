using System.ComponentModel.DataAnnotations;

namespace DigiCV.Domain.Entities
{
    public class Education:IEntity<int>
    {
        public int Id { get; set; }   
        public string Level { get; set; }   
        public string University { get; set; }
        public string Grade { get; set; }
        public DateTime PassingYear { get; set; }
        public Guid ResumeId { get; set; }
        public Resume? Resume { get; set; }
    }
}
