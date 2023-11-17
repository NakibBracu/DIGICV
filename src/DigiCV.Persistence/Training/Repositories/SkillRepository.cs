using DigiCV.Application.Features.Training.Repositories;
using DigiCV.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Persistence.Training.Repositories
{
    public class SkillRepository : Repository<Skill, int>, ISkillRepository
    {
        public SkillRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<(IList<Skill> records, int total, int totalDisplay)> GetTableDataAsync(Expression<Func<Skill, bool>> expression, string orderBy, int pageIndex, int pageSize)
        {
            return await GetDynamicAsync(expression, orderBy, null, pageIndex, pageSize, true);
        }

        public bool IsDuplicateName(string name, int? id)
        {
            int? existingSkillCount = null;
            if (id.HasValue)
                existingSkillCount = GetCount(x => x.Name == name && x.Id != id);
            else
                existingSkillCount = GetCount(x => x.Name == name);

            return existingSkillCount > 0;
        }

    }
}