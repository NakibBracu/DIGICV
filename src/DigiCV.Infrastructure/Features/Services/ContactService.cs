using DigiCV.Application;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Infrastructure.Features.Services
{
    public class ContactService : IContactService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public ContactService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IList<Contact> GetContacts()
        {
            return _unitOfWork.Contacts.GetAll();
        }

        public Contact GetContact(Guid id)
        {
            return _unitOfWork.Contacts.GetById(id);
        }

        public void CreateContact(string name, string email, string subject, string message)
        { 
            Contact contact = new Contact()
            {
                Name = name,
                Email = email,
                Subject = subject,
                Message = message
            };

            _unitOfWork.Contacts.Add(contact);
            _unitOfWork.Save();
        }

        public void DeleteContact(Guid id)
        {
            _unitOfWork.Contacts.Remove(id);
            _unitOfWork.Save();
        }

        public async Task<(IList<Contact> records, int total, int totalDisplay)> GetPagedContactsAsync(int pageIndex, 
            int pageSize, string searchText, string orderBy)
        {
            var result = await _unitOfWork.Contacts.GetTableDataAsync(
                 x => x.Name.Contains(searchText), orderBy, pageIndex, pageSize);

            return result;
        }
    }
}
