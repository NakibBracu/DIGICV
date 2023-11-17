using Autofac;
using AutoMapper;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using DigiCV.Infrastructure;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace DigiCV.API.Models
{
    public class ContactModel
    {
        private IMapper _mapper;
        private IContactService _contactService;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        public ContactModel()
        {

        }

        public ContactModel(IMapper mapper, IContactService contactService)
        {
            _mapper = mapper;
            _contactService = contactService;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _contactService = scope.Resolve<IContactService>();
            _mapper = scope.Resolve<IMapper>();
        }
        internal IList<Contact>? GetContacts()
        {
            return _contactService?.GetContacts();
        }

        internal Contact? GetContact(Guid id)
        {
            return _contactService?.GetContact(id);
        }

        internal void CreateContact()
        {
            _contactService?.CreateContact(Name, Email, Subject, Message);
        }

        internal void DeleteContact(Guid id)
        {
            _contactService?.DeleteContact(id);
        }

        internal async Task<object?> GetPagedContactsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {

            var data = await _contactService.GetPagedContactsAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,
                dataTablesUtility.SearchText,
                dataTablesUtility.GetSortText(new string[] { "Name", "Email", "Subject", "Message" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Email.ToString(),
                                record.Subject.ToString(),
                                record.Message.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };

            
        }

    }
}
