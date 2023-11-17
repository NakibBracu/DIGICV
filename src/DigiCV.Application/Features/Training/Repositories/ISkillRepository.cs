using DigiCV.Domain.Entities;
using DigiCV.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Application.Features.Training.Repositories
{
    public interface ISkillRepository : IRepositoryBase<Skill, int>
    {
        Task<(IList<Skill> records, int total, int totalDisplay)>
            GetTableDataAsync(Expression<Func<Skill, bool>> expression, string orderBy, int pageIndex, int pageSize);

            bool IsDuplicateName(string name, int? id);
    }
}