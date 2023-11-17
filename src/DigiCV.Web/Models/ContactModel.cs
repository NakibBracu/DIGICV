using System.ComponentModel.DataAnnotations;

namespace DigiCV.Web.Models
{
    public class ContactModel
    {

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }


    }
}
