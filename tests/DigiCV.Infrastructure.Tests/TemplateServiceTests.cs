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
    public class TemplateServiceTests
    {
        private AutoMock _mock;
        private Mock<IApplicationUnitOfWork> _applicationUnitOfWork;
        private Mock<IResumeTemplateRepository> _resumeTemplateRepositoryMock;
        private ITemplateService _templateService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _applicationUnitOfWork = _mock.Mock<IApplicationUnitOfWork>();
            _resumeTemplateRepositoryMock = _mock.Mock<IResumeTemplateRepository>();
            _templateService = _mock.Create<TemplateService>();
        }

        [TearDown]
        public void Teardown()
        {
            _applicationUnitOfWork.Reset();
            _resumeTemplateRepositoryMock.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public void CreateTemplate_TemplateDoesNotExists_CreateTemplate()
        {
            // Arrange
            const string name = "Modern";
            const string imageName = "modern.png";
            const bool isActive = true;

            _applicationUnitOfWork.Setup(x => x.ResumeTemplates)
                .Returns(_resumeTemplateRepositoryMock.Object);

            _resumeTemplateRepositoryMock.Setup(x => x.IsDuplicateName(name, null))
                .Returns(false).Verifiable();

            _applicationUnitOfWork.Setup(x => x.Save()).Verifiable();

            _resumeTemplateRepositoryMock.Setup(x => x.Add(
               It.Is<ResumeTemplate>(y => y.Name == name && y.ImageName == imageName && y.IsActive == isActive)))
               .Verifiable();

            // Act
            _templateService.CreateTemplate(name, imageName, isActive);

            // Assert
            this.ShouldSatisfyAllConditions(_applicationUnitOfWork.VerifyAll,
                _resumeTemplateRepositoryMock.VerifyAll);
        }

        [Test]
        public void CreateTemplate_Exist_ThrowsError()
        {
            // Arrange
            const string name = "Modern";
            const string imageName = "modern.png";
            const bool isActive = true;

            _applicationUnitOfWork.Setup(x => x.ResumeTemplates)
                .Returns(_resumeTemplateRepositoryMock.Object);

            _resumeTemplateRepositoryMock.Setup(x => x.IsDuplicateName(name, null))
                .Returns(true);

            // Act
            Should.Throw<DuplicateNameException>(
                () => _templateService.CreateTemplate(name, imageName, isActive));
        }

        [Test]
        public void GetTemplates_TemplatesExist_ReturnListOfTemplates()
        {
            // Arrange
            List<ResumeTemplate> templates = new List<ResumeTemplate>
            {
                new ResumeTemplate
                {
                    Id = Guid.NewGuid(),
                    Name = "Classic",
                    ImageName = "classic.png",
                    IsActive = true
                },
                new ResumeTemplate
                {
                    Id = Guid.NewGuid(),
                    Name = "Modern",
                    ImageName = "modern.png",
                    IsActive = false
                },
                new ResumeTemplate
                {
                    Id = Guid.NewGuid(),
                    Name = "Standard",
                    ImageName = "standard.png",
                    IsActive = true
                }
            };

            _resumeTemplateRepositoryMock.Setup(x => x.GetAll())
                .Returns(templates);

            _applicationUnitOfWork.Setup(x => x.ResumeTemplates)
                .Returns(_resumeTemplateRepositoryMock.Object);

            // Act
            var result = _templateService.GetTemplates();

            // Assert
            _resumeTemplateRepositoryMock.Verify(x => x.GetAll(), Times.Once);

            // Verify that the result is not null and contains the same templates as expected
            result.ShouldNotBeNull();
            result.ShouldBeEquivalentTo(templates);
        }

        [Test]
        public void GetTemplateById_ValidId_ReturnTemplate()
        {
            // Arrange
            var id = Guid.NewGuid();
            ResumeTemplate template = new ResumeTemplate
            {
                Id = id,
                Name = "Standard",
                ImageName = "standard.png",
                IsActive = true
            };

            _applicationUnitOfWork.Setup(x => x.ResumeTemplates)
                .Returns(_resumeTemplateRepositoryMock.Object);

            _resumeTemplateRepositoryMock.Setup(x => x.GetById(id))
                .Returns(template);

            // Act
            var result = _templateService.GetTemplateById(id);

            // Assert
            _resumeTemplateRepositoryMock.Verify(x => x.GetById(id), Times.Once);

            // Verify that the result matches the expected template
            result.ShouldNotBeNull();
            result.Id.ShouldBe(id);
            result.Name.ShouldBe(template.Name);
            result.ImageName.ShouldBe(template.ImageName);
            result.IsActive.ShouldBe(template.IsActive);
        }

        [Test]
        public void DeleteTemplate_TemplateExists_DeletesTemplate()
        {
            // Arrange
            var id = Guid.NewGuid();
            ResumeTemplate template = new ResumeTemplate
            {
                Id = id,
                Name = "Standard",
                ImageName = "standard.png",
                IsActive = true
            };

            _applicationUnitOfWork.Setup(x => x.ResumeTemplates)
                .Returns(_resumeTemplateRepositoryMock.Object);

            _applicationUnitOfWork.Setup(x => x.Save()).Verifiable();

            _resumeTemplateRepositoryMock.Setup(x => x.Remove(id)).Verifiable();

            // Act
            _templateService.DeleteTemplate(id);

            // Assert
            this.ShouldSatisfyAllConditions(_applicationUnitOfWork.VerifyAll,
                _resumeTemplateRepositoryMock.VerifyAll);
        }

        [Test]
        public async Task GetPagedResumeTemplatesAsync_ValidParameters_ReturnsPagedTemplates()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 10;
            var searchText = "Template";
            var orderBy = "Name";

            var templates = new List<ResumeTemplate>
            {
                new ResumeTemplate { Id = Guid.NewGuid(), Name = "Classic", ImageName = "classic.png", IsActive = true },
                new ResumeTemplate { Id = Guid.NewGuid(), Name = "Modern", ImageName = "modern.png", IsActive = true },
                new ResumeTemplate { Id = Guid.NewGuid(), Name = "Standard", ImageName = "standard.png", IsActive = false }
            };

            var totalRecords = templates.Count;

            _applicationUnitOfWork.Setup(x => x.ResumeTemplates)
                .Returns(_resumeTemplateRepositoryMock.Object);

            _resumeTemplateRepositoryMock.Setup(x => x.GetTableDataAsync(
                It.IsAny<Expression<Func<ResumeTemplate, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .ReturnsAsync((templates, totalRecords, templates.Count))
                .Verifiable();

            // Act
            var result = await _templateService.GetPagedResumeTemplatesAsync(pageIndex, pageSize, searchText, orderBy);

            // Assert
            _applicationUnitOfWork.Verify(x => x.ResumeTemplates, Times.Once);
            _resumeTemplateRepositoryMock.Verify(x => x.GetTableDataAsync(
                It.IsAny<Expression<Func<ResumeTemplate, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()), Times.Once);

            result.records.ShouldNotBeNull();
            result.records.ShouldBeEquivalentTo(templates);
            result.total.ShouldBe(totalRecords);
            result.totalDisplay.ShouldBe(templates.Count);
        }

        [Test]
        public void UpdateResumeTemplate_TemplateExists_UpdatesTemplate()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = "Classic";
            string imageName = "classic.png";
            bool isActive = true;

            _resumeTemplateRepositoryMock.Setup(x => x.GetById(id))
                .Returns(new ResumeTemplate { Id = id });

            _applicationUnitOfWork.Setup(x => x.ResumeTemplates)
                .Returns(_resumeTemplateRepositoryMock.Object);

            _applicationUnitOfWork.Setup(x => x.Save()).Verifiable();

            // Act
            _templateService.UpdateResumeTemplate(id, name, imageName, isActive);

            // Assert
            _resumeTemplateRepositoryMock.Verify(x => x.GetById(id));
            _applicationUnitOfWork.Verify(x => x.Save());
        }
    }
}