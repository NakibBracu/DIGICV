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
    public class UserProfileRepository : Repository<UserProfile, Guid>, IUserProfileRepository
    {
        public UserProfileRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<(IList<UserProfile> records, int total, int totalDisplay)> GetTableDataAsync(Expression<Func<UserProfile, bool>> expression, string orderBy, int pageIndex, int pageSize)
        {
            return await GetDynamicAsync(expression, orderBy, null, pageIndex, pageSize, true);
        }
    }
}