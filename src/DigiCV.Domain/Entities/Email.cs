using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Domain.Entities
{
    public class Email : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsSent { get; set; }
    }
}
