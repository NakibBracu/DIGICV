using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;

namespace DigiCV.Web.Models
{
    public class ResumeViewModel
    {
        private readonly IResumeService _ResumeService;
        
        public Resume ResumeProperty { get; set; }

        public ResumeViewModel() { }

        public ResumeViewModel(IResumeService ResumeService)
        {
            _ResumeService = ResumeService;       
        }
     
        public async Task GetResumeDataAsync(Guid Id)
        {
            var ResumeData = await _ResumeService.GetResume(Id);
            ResumeProperty = ResumeData;
        }

        public async Task<string> GetResumeTemplateName(Guid Id)
        {
            var ResumeData = await _ResumeService.GetResume(Id);
            var templateId = ResumeData.ResumeTemplateId ?? Guid.Empty;
            var TemplateName = await _ResumeService.GetTemplateNameAsync(templateId);

            return TemplateName+ "PDF";
        }


        public async Task<string> GetResumeViewName(Guid ResumeTemplateId) { 
          var TemplateName = await _ResumeService.GetTemplateNameAsync(ResumeTemplateId);

          return TemplateName;
        }

        public async Task GetResumeDataAsync(Guid userId, string resumeTitle)
        {
            var resumeData = await _ResumeService.GetResume(userId, resumeTitle);
            ResumeProperty = resumeData;
        }
    }
}
