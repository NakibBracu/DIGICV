using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Domain.Entities
{
    public class CoverLetter : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public DateTime SendingDate { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set;}
        public string SenderPhone { get; set;}
        public string SenderAddress { get; set; }
        public string SenderAddressEx { get; set;}
        public string Recipient { get; set; }
        public string CompanyName { get; set;}
        public string CompanyAddress { get; set;}
        public string Subject { get; set; }
        public string RecipientAddressing { get; set; }
        public string Body { get; set; }

    }
}
