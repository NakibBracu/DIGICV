using DigiCV.Application.Features.Training.Repositories;
using DigiCV.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DigiCV.Persistence.Training.Repositories
{
    public class CoverLetterRepository : Repository<CoverLetter, Guid>, ICoverLetterRepository
    {
        public CoverLetterRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }
        public async Task<(IList<CoverLetter> records, int total, int totalDisplay)>
            GetTableDataAsync(Expression<Func<CoverLetter, bool>> expression, string orderBy, int pageIndex, int pageSize)
        {
            return await GetDynamicAsync(expression, orderBy, null, pageIndex, pageSize, true);
        }

        public bool IsDuplicateName(string name, Guid? id)
        {
            int? ExistingCoverLetterCount = null;

            if (id.HasValue)
                ExistingCoverLetterCount = GetCount(x => x.Title == name && x.Id != id.Value);
            else
                ExistingCoverLetterCount = GetCount(x => x.Title == name);

            return ExistingCoverLetterCount > 0;
        }
        public IList<(Guid Id, string Title)> GetCoverLettersByUserId(Guid userId)
        {
            return Get(x => x.UserId == userId).Select(r => (r.Id, r.Title)).ToList();
        }
    }
}
