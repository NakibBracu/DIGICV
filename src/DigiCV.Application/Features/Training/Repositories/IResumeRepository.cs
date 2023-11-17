using DigiCV.Domain;
using DigiCV.Domain.Entities;
using System.Linq.Expressions;

namespace DigiCV.Application.Features.Training.Repositories
{
    public interface IResumeRepository : IRepositoryBase<Resume, Guid>
    {
        Task<Resume> GetByResumeTitleAsync(Guid userId, string resumeTitle);
        IList<(Guid resumeId, string title)> GetResumeByUserId(Guid userId);
        Task<(IList<Resume> records, int total, int totalDisplay)> GetTableDataAsync(
            Expression<Func<Resume, bool>> expression,
            string orderBy, int pageIndex, int pageSize);
    }
}