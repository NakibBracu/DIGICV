using Autofac;
using DigiCV.Application.Features.Training.Services;
using System.ComponentModel.DataAnnotations;
using DigiCV.Domain.Entities;

namespace DigiCV.Web.Areas.Admin.Models
{
    public class TemplateUpdateModel
    {
        private ITemplateService _templateService;
        public TemplateUpdateModel() { }
        public TemplateUpdateModel(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ImageName { get; set; }
        public bool IsActive { get; set; }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _templateService = scope.Resolve<ITemplateService>();
        }

        internal void Load(Guid id)
        {
            ResumeTemplate resumeTemplate = _templateService.GetTemplateById(id);
            Name = resumeTemplate.Name;
            ImageName = resumeTemplate.ImageName;
            IsActive = resumeTemplate.IsActive;
        }

        internal void Update()
        {
            _templateService.UpdateResumeTemplate(Id, Name, ImageName, IsActive);
        }
    }
}
