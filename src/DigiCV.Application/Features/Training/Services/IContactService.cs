using DigiCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Application.Features.Training.Services
{
    public interface IContactService
    {
        public IList<Contact> GetContacts();
        Contact GetContact(Guid id);
        void CreateContact(string name, string email, string subject, string message);
        void DeleteContact(Guid id);
        Task<(IList<Contact> records, int total, int totalDisplay)> GetPagedContactsAsync(int pageIndex,
            int pageSize, string searchText, string orderBy);
    }
}
