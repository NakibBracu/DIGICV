using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DigiCV.Persistence.Features.Membership;
using DigiCV.Domain.Entities;
using DigiCV.Infrastructure;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Infrastructure.Features.Services;
using System.Web;

namespace DigiCV.Web.Areas.Admin.Models
{
    public class SkillListModel
    {
        private ISkillService _skillService;
        public SkillListModel() { }
        public SkillListModel(ISkillService skillService)
        {
            _skillService = skillService;
        }
        public IList<Skill> GetAllSkill()
        {
            return _skillService.GetSkills();
        }
        public async Task<object> GetPagedSkillsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _skillService.GetPagedSkillsAsync(
            dataTablesUtility.PageIndex,
            dataTablesUtility.PageSize,
            dataTablesUtility.SearchText,
            dataTablesUtility.GetSortText(new string[] { "Name" }));
            
            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Delete(int id)
        {
            _skillService.DeleteSkill(id);
        }
    }
}
