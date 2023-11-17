using DigiCV.Domain;
using DigiCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Application.Features.Training.Repositories
{
    public interface IResumeTemplateRepository : IRepositoryBase<ResumeTemplate, Guid>
    {
        Task<(IList<ResumeTemplate> records, int total, int totalDisplay)>
            GetTableDataAsync(Expression<Func<ResumeTemplate, bool>> expression, string orderBy, int pageIndex, int pageSize);
        bool IsDuplicateName(string name, Guid? id);
    }
}
