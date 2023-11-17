using DigiCV.Application.Features.Training.Repositories;
using DigiCV.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DigiCV.Persistence.Training.Repositories
{
    public class ContactRepository : Repository<Contact, Guid>, IContactRepository
    {
        public ContactRepository(IApplicationDbContext context) : base((DbContext)context) { }

        public async Task<(IList<Contact> records, int total, int totalDisplay)>
            GetTableDataAsync(Expression<Func<Contact, bool>> expression,
            string orderBy, int pageIndex, int pageSize)
        {
            return await GetDynamicAsync(expression, orderBy, null,
                pageIndex, pageSize, true);
        }
    }
}
