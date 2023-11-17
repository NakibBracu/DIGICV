using DigiCV.Domain.Entities;
using DigiCV.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DigiCV.Application.Features.Training.Repositories
{
    public interface ICoverLetterRepository : IRepositoryBase<CoverLetter, Guid>
    {
        Task<(IList<CoverLetter> records, int total, int totalDisplay)>
            GetTableDataAsync(Expression<Func<CoverLetter, bool>> expression, string orderBy,
            int pageIndex, int pageSize);
        bool IsDuplicateName(string name, Guid? id);
        public IList<(Guid Id, string Title)> GetCoverLettersByUserId(Guid userId);
    }
}
