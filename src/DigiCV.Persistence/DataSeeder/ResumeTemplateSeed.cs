using DigiCV.Domain.Entities;
using DigiCV.Persistence.Features.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Persistence.DataSeeder
{
    public static class ResumeTemplateSeed
    {
        public static IList<ResumeTemplate> ResumeTemplateList()
        {
            var resumeTemplate = new List<ResumeTemplate>()
            {
                new ResumeTemplate()
                {
                    Id = new Guid("933B1BDB-19DF-4E24-BF1F-49F5ABD8AEF6"),
                    Name= "Modern",
                    ImageName = "34a90fa4-ce8a-499c-8182-852a7125141f.png",
                    IsActive = true
                },
                new ResumeTemplate()
                {
                    Id = new Guid("933B1BDB-19DF-4E24-BF1F-49F5ABD8AEF7"),
                    Name = "Standard",
                    ImageName = "22ce39f1-a576-4e66-a9b3-312552c95dde.png",
                    IsActive = true
                }
            };
            return resumeTemplate;
        }
    }
}
