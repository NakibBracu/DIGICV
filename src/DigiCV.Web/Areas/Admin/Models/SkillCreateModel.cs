using Autofac;
using System.ComponentModel.DataAnnotations;
using DigiCV.Application.Features.Training.Services;

namespace DigiCV.Web.Areas.Admin.Models
{
    public class SkillCreateModel
    {
        private ISkillService _skillService;
        public SkillCreateModel() { }
        public SkillCreateModel(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [Required]
        public string Name { get; set; }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _skillService = scope.Resolve<ISkillService>();
        }
        internal void Create()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                _skillService.CreateSkill(Name);
            
            }
        }
    }
}
