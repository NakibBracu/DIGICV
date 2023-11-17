using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Infrastructure.EmailTemplates
{
    public partial class EmailConfirmationTemplate
    {
        private string Name { get; set; }
        private string Link { get; set; }

        public EmailConfirmationTemplate(string name, string link)
        {
            Name = name;
            Link = link;
        }
    }
}
