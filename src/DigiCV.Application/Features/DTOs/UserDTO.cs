using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Application.Features.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string? ImageUrl { get; set; }
        public string ClaimType { get; set; }
        public bool IsActive { get; set; }
    }
}
