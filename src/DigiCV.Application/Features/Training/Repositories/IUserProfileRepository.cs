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
    public interface IUserProfileRepository : IRepositoryBase<UserProfile, Guid>
    {
        Task<(IList<UserProfile> records, int total, int totalDisplay)>
            GetTableDataAsync(Expression<Func<UserProfile, bool>> expression, string orderBy, int pageIndex, int pageSize);
    }
}