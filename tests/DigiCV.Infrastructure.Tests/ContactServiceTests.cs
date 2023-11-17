using Autofac.Extras.Moq;
using DigiCV.Application;
using DigiCV.Application.Features.Training.Repositories;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using DigiCV.Infrastructure.Features.Exceptions;
using DigiCV.Infrastructure.Features.Services;
using Moq;
using Shouldly;
using System.Linq.Expressions;

namespace DigiCV.Infrastructure.Tests
{
    public class ContactServiceTests
    {
        private AutoMock _mock;
        private Mock<IApplicationUnitOfWork> _applicationUnitOfWork;
        private Mock<IContactRepository> _contactRepositoryMock;
        private IContactService _contactService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _applicationUnitOfWork = _mock.Mock<IApplicationUnitOfWork>();
            _contactRepositoryMock = _mock.Mock<IContactRepository>();
            _contactService = _mock.Create<ContactService>();
        }

        [TearDown]
        public void Teardown()
        {
            _applicationUnitOfWork.Reset();
            _contactRepositoryMock.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public void GetContacts_ContactsExist_ReturnListOfContacts()
        {
            // Arrange
            List<Contact> contacts = new List<Contact>
            {
                new Contact
                {
                    Id = Guid.NewGuid(),
                    Name = "Shoyeb",
                    Email = "shoyeb@digicv.com",
                    Subject = "Inquiry",
                    Message = "Hello, I have a question."
                },
                new Contact
                {
                    Id = Guid.NewGuid(),
                    Name = "Shoyeb2",
                    Email = "shoyeb2@digicv.com",
                    Subject = "Feedback",
                    Message = "Great service!"
                }
            };

            _contactRepositoryMock.Setup(x => x.GetAll())
                .Returns(contacts);

            _applicationUnitOfWork.Setup(x => x.Contacts)
                .Returns(_contactRepositoryMock.Object);

            // Act
            var result = _contactService.GetContacts();

            // Assert
            _contactRepositoryMock.Verify(x => x.GetAll(), Times.Once);

            // Verify that the result is not null and contains the same contacts as expected
            result.ShouldNotBeNull();
            result.ShouldBeEquivalentTo(contacts);
        }

        [Test]
        public void GetContact_ValidId_ReturnContact()
        {
            // Arrange
            var id = Guid.NewGuid();
            Contact contact = new Contact
            {
                Id = id,
                Name = "Shoyeb",
                Email = "shoyeb@digicv.com",
                Subject = "Inquiry",
                Message = "Hello, I have a question."
            };

            _contactRepositoryMock.Setup(x => x.GetById(id))
                .Returns(contact);

            _applicationUnitOfWork.Setup(x => x.Contacts)
                .Returns(_contactRepositoryMock.Object);

            // Act
            var result = _contactService.GetContact(id);

            // Assert
            _contactRepositoryMock.Verify(x => x.GetById(id), Times.Once);
            // Verify that the result matches the expected Contact
            result.ShouldNotBeNull();
            result.Id.ShouldBe(id);
            result.Name.ShouldBe(contact.Name);
            result.Email.ShouldBe(contact.Email);
            result.Subject.ShouldBe(contact.Subject);
            result.Message.ShouldBe(contact.Message);
        }

        [Test]
        public void CreateContact_ValidData_CreatesContact()
        {
            // Arrange
            const string name = "Shoyeb";
            const string email = "shoyeb@digicv.com";
            const string subject = "Inquiry";
            const string message = "Hello, I have a question.";

            _applicationUnitOfWork.Setup(x => x.Contacts)
                .Returns(_contactRepositoryMock.Object);

            _applicationUnitOfWork.Setup(x => x.Save()).Verifiable();

            _contactRepositoryMock.Setup(x => x.Add(
               It.Is<Contact>(y => y.Name == name && y.Email == email && y.Subject == subject && y.Message == message)))
               .Verifiable();

            // Act
            _contactService.CreateContact(name, email, subject, message);

            // Assert
            this.ShouldSatisfyAllConditions(_applicationUnitOfWork.VerifyAll,
                _contactRepositoryMock.VerifyAll);
        }

        [Test]
        public void DeleteContact_ContactExists_DeletesContact()
        {
            // Arrange
            var id = Guid.NewGuid();
            Contact contact = new Contact
            {
                Id = id,
                Name = "Shoyeb",
                Email = "shoyeb@digicv.com",
                Subject = "Inquiry",
                Message = "Hello, I have a question."
            };

            _applicationUnitOfWork.Setup(x => x.Contacts)
                .Returns(_contactRepositoryMock.Object);

            _applicationUnitOfWork.Setup(x => x.Save()).Verifiable();

            _contactRepositoryMock.Setup(x => x.Remove(id)).Verifiable();

            // Act
            _contactService.DeleteContact(id);

            // Assert
            this.ShouldSatisfyAllConditions(_applicationUnitOfWork.VerifyAll,
                _contactRepositoryMock.VerifyAll);
        }

        [Test]
        public async Task GetPagedContactsAsync_ValidParameters_ReturnsPagedContacts()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 10;
            var searchText = "Shoyeb";
            var orderBy = "Name";

            var contacts = new List<Contact>
            {
                new Contact { Id = Guid.NewGuid(), Name = "Shoyeb" },
                new Contact { Id = Guid.NewGuid(), Name = "Shoyeb2" },
                new Contact { Id = Guid.NewGuid(), Name = "Shoyeb3" }
            };

            var totalRecords = contacts.Count;

            _applicationUnitOfWork.Setup(x => x.Contacts)
                .Returns(_contactRepositoryMock.Object);

            _contactRepositoryMock.Setup(x => x.GetTableDataAsync(
                It.IsAny<Expression<Func<Contact, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .ReturnsAsync((contacts, totalRecords, contacts.Count))
                .Verifiable();

            // Act
            var result = await _contactService.GetPagedContactsAsync(pageIndex, pageSize, searchText, orderBy);

            // Assert
            _applicationUnitOfWork.Verify(x => x.Contacts, Times.Once);
            _contactRepositoryMock.Verify(x => x.GetTableDataAsync(
                It.IsAny<Expression<Func<Contact, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()), Times.Once);

            result.records.ShouldNotBeNull();
            result.records.ShouldBeEquivalentTo(contacts);
            result.total.ShouldBe(totalRecords);
            result.totalDisplay.ShouldBe(contacts.Count);
        }
    }
}