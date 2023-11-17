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
    public class ResumeTemplateRepository : Repository<ResumeTemplate, Guid>, IResumeTemplateRepository
    {
        public ResumeTemplateRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }
        public async Task<(IList<ResumeTemplate> records, int total, int totalDisplay)> GetTableDataAsync(Expression<Func<ResumeTemplate, bool>> expression, string orderBy, int pageIndex, int pageSize)
        {
            return await GetDynamicAsync(expression, orderBy, null, pageIndex, pageSize, true);
        }

        public bool IsDuplicateName(string name, Guid? id)
        {
            int? existingResumeTemplateCount = null;

            if (id.HasValue)
                existingResumeTemplateCount = GetCount(x => x.Name == name && x.Id != id.Value);
            else
                existingResumeTemplateCount = GetCount(x => x.Name == name);

            return existingResumeTemplateCount > 0;
        }
    }
}
