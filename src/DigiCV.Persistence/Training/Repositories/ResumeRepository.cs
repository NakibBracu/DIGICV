using DigiCV.Application.Features.Training.Repositories;
using DigiCV.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DigiCV.Persistence.Training.Repositories
{
    public class ResumeRepository : Repository<Resume, Guid>, IResumeRepository
    {
        public ResumeRepository(IApplicationDbContext context) : base((DbContext) context)
        {
        }

        public async Task<Resume> GetByResumeTitleAsync(Guid userId, string resumeTitle)
        {
            return  GetAll()
                    .Where(resume => resume.UserId == userId && resume.Title.ToLower() == resumeTitle.ToLower())
                    .FirstOrDefault();
        }
        public IList<(Guid resumeId, string title)>GetResumeByUserId(Guid userId)
        {
            return Get(x=>x.UserId==userId).Select(r=>(r.Id, r.Title)).ToList();
        }
        public Task<(IList<Resume> records, int total, int totalDisplay)> GetTableDataAsync(Expression<Func<Resume, bool>> expression, string orderBy, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
