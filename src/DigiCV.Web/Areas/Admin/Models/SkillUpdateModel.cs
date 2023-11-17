using Autofac;
using DigiCV.Application.Features.Training.Services;
using System.ComponentModel.DataAnnotations;
using DigiCV.Domain.Entities;

namespace DigiCV.Web.Areas.Admin.Models
{
    public class SkillUpdateModel
    {
        private ISkillService _skillService;
        public SkillUpdateModel() { }
        public SkillUpdateModel(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _skillService = scope.Resolve<ISkillService>();
        }

        internal void Load(int id)
        {
            Skill skill = _skillService.GetSkill(id);
            Id = skill.Id;
            Name = skill.Name;
        }

        internal void Update()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                _skillService.UpdateSkill(Id, Name);
            }
        }
    }
}
