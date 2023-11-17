using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Domain.Entities
{
    public class Template : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public string ImageURL { get; set; }    
    }

}
