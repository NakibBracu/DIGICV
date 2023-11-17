using System.ComponentModel.DataAnnotations;

namespace DigiCV.Domain.Entities
{
    public class Reference:IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Compnay { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid ResumeId { get; set; }
        public Resume? Resume { get; set; }
    }
}



