using DigiCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Application.Features.Training.Services
{
    public interface ISkillService
    {
        void CreateSkill(string name);
        void DeleteSkill(int id);
        Skill GetSkill(int id);
        public IList<Skill> GetSkills();
        Task<(IList<Skill> records, int total, int totalDisplay)> GetPagedSkillsAsync(int pageIndex,
            int pageSize, string searchText, string orderBy);
        void UpdateSkill(int id, string name);
    }
}
