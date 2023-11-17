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
    public interface IContactRepository : IRepositoryBase<Contact, Guid>
    {
        Task<(IList<Contact> records, int total, int totalDisplay)>
            GetTableDataAsync(Expression<Func<Contact, bool>> expression, string orderBy,
            int pageIndex, int pageSize);
    }
}
