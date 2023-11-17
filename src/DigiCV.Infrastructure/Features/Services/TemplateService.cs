using DigiCV.Application;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using DigiCV.Infrastructure.Features.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DigiCV.Infrastructure.Features.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public TemplateService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateTemplate(string name, string imageName, bool isActive)
        {
            if (_unitOfWork.ResumeTemplates.IsDuplicateName(name, null))
                throw new DuplicateNameException("Template name is duplicate");

            ResumeTemplate resumeTemplate = new ResumeTemplate()
            {
                Name = name,
                ImageName = imageName,
                IsActive = isActive
            };

            _unitOfWork.ResumeTemplates.Add(resumeTemplate);
            _unitOfWork.Save();
        }

        public IList<ResumeTemplate> GetTemplates()
        {
            return _unitOfWork.ResumeTemplates.GetAll();
        }
        public ResumeTemplate GetTemplateById(Guid Id)
        {
            return _unitOfWork.ResumeTemplates.GetById(Id);
        }
        public void DeleteTemplate(Guid id)
        {
            _unitOfWork.ResumeTemplates.Remove(id);
            _unitOfWork.Save();
        }

        public async Task<(IList<ResumeTemplate> records, int total, int totalDisplay)> GetPagedResumeTemplatesAsync(int pageIndex,
        int pageSize, string searchText, string orderBy)
        {
            var result = await _unitOfWork.ResumeTemplates.GetTableDataAsync(
                x => x.Name.Contains(searchText), orderBy, pageIndex, pageSize);
            return result;
        }

        public void UpdateResumeTemplate(Guid id, string name, string imageName, bool isActive)
        {
            ResumeTemplate resumeTemplate = _unitOfWork.ResumeTemplates.GetById(id);
            resumeTemplate.Name = name;
            resumeTemplate.ImageName = imageName;
            resumeTemplate.IsActive = isActive;
            _unitOfWork.Save();
        }

        public int GetTotalResumeTemplateCount()
        {
            var totalTemplate = _unitOfWork.ResumeTemplates.GetCount();
            return totalTemplate;
        }
    }
}
