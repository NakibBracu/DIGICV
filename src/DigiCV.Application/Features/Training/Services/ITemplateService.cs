using DigiCV.Domain.Entities;
using DigiCV.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DigiCV.Application.Features.Training.Services
{
    public interface ITemplateService
    {
        public IList<ResumeTemplate> GetTemplates();
        void CreateTemplate(string name, string imageName, bool isActive);
        ResumeTemplate GetTemplateById(Guid id);
        void DeleteTemplate(Guid id);
        void UpdateResumeTemplate(Guid id, string name, string imageName, bool isActive);
        Task<(IList<ResumeTemplate> records, int total, int totalDisplay)> GetPagedResumeTemplatesAsync(int pageIndex,
            int pageSize, string searchText, string orderBy);
        int GetTotalResumeTemplateCount();
    }
}
