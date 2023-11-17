using Autofac.Extras.Moq;
using DigiCV.Application;
using DigiCV.Application.Features.DTOs;
using DigiCV.Application.Features.Training.Repositories;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using DigiCV.Domain.Utilities;
using DigiCV.Infrastructure.Features.Exceptions;
using DigiCV.Infrastructure.Features.Services;
using Moq;
using Shouldly;
using System.Linq.Expressions;

namespace DigiCV.Infrastructure.Tests
{
    public class ResumeServiceTests
    {
        private AutoMock _mock;
        private Mock<IApplicationUnitOfWork> _applicationUnitOfWork;
        private Mock<IResumeRepository> _resumeRepositoryMock;
        private Mock<IResumeTemplateRepository> _resumeTemplateRepositoryMock;
        private Mock<IAdoNetUtility> _adoNetUtility;
        private Mock<IUserProfileRepository> _userRepositoryMock;
        private IResumeService _resumeService;
        private IUserService _userService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _applicationUnitOfWork = _mock.Mock<IApplicationUnitOfWork>();
            _resumeRepositoryMock = _mock.Mock<IResumeRepository>();
            _resumeTemplateRepositoryMock = _mock.Mock<IResumeTemplateRepository>();
            _adoNetUtility = _mock.Mock<IAdoNetUtility>();
            _userRepositoryMock = _mock.Mock<IUserProfileRepository>();
            _resumeService = _mock.Create<ResumeService>();
            _userService = _mock.Create<UserService>();
        }

        [TearDown]
        public void Teardown()
        {
            _applicationUnitOfWork.Reset();
            _resumeRepositoryMock.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task CreateResume_NewResume_CreatesResume()
        {
            // Arrange
            Resume resume = new Resume
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                ResumeTemplateId = Guid.NewGuid(),
                Title = "Software Developer",
                FullName = "Shoyeb",
                Email = "shoyeb@digicv.com",
                PhoneNumber = "+8801076734741",
                Skype = "msashoyeb",
                LinkedIn = "https://www.linkedin.com/in/msashoyeb",
                Address = "Puran Dhaka",
                Summary = "Experienced software developer with expertise in C# and .NET",
                ImageName = "shoyeb.jpg",
                Trainings = new List<string> { "C# Fundamentals", "ASP.NET Core", "Azure Fundamentals" },
                ResumeTemplate = new ResumeTemplate { Id = Guid.NewGuid(), Name = "Modern" },
                Skills = new List<ResumeSkill> { new ResumeSkill { SkillId = 1, ResumeId = Guid.NewGuid() }, new ResumeSkill { SkillId = 1, ResumeId = Guid.NewGuid() } },
                Experiences = new List<Experience> { new Experience { Id = 1 } },
                Projects = new List<Project> { new Project { Id = 1 } },
                Educations = new List<Education> { new Education { Id = 1 } },
                References = new List<Reference> { new Reference {Id = 1 } }
            };

            _applicationUnitOfWork.Setup(x => x.Resumes)
                .Returns(_resumeRepositoryMock.Object);

            _resumeRepositoryMock.Setup(x => x.AddAsync(resume))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _applicationUnitOfWork.Setup(x => x.SaveAsync())
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var result = await _resumeService.CreateResume(resume);

            // Assert
            _resumeRepositoryMock.VerifyAll();
            _applicationUnitOfWork.VerifyAll();

            result.ShouldNotBe(Guid.Empty);
        }

        [Test]
        public async Task DeleteResume_ExistingResume_DeletesResume()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            _applicationUnitOfWork.Setup(x => x.Resumes)
                .Returns(_resumeRepositoryMock.Object);

            _resumeRepositoryMock.Setup(x => x.RemoveAsync(id))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _applicationUnitOfWork.Setup(x => x.SaveAsync())
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            await _resumeService.DeleteResume(id);

            // Assert
            _resumeRepositoryMock.VerifyAll();
            _applicationUnitOfWork.VerifyAll();
        }

        [Test]
        public async Task GetResume_ExistingResume_ReturnsResume()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Resume resume = new Resume();

            _applicationUnitOfWork.Setup(x => x.Resumes)
                .Returns(_resumeRepositoryMock.Object);

            _resumeRepositoryMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(resume);

            // Act
            var result = await _resumeService.GetResume(id);

            // Assert
            _resumeRepositoryMock.Verify(x => x.GetByIdAsync(id), Times.Once);

            result.ShouldNotBeNull();
            result.ShouldBe(resume);
        }

        [Test]
        public async Task GetResumes_ReturnsListOfResumes()
        {
            // Arrange
            List<Resume> resumes = new List<Resume>
            {
                new Resume { Id = Guid.NewGuid() },
                new Resume { Id = Guid.NewGuid() },
                new Resume { Id = Guid.NewGuid() }
            };

            _applicationUnitOfWork.Setup(x => x.Resumes)
                .Returns(_resumeRepositoryMock.Object);

            _resumeRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(resumes);

            // Act
            var result = await _resumeService.GetResumes();

            // Assert
            _resumeRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);

            result.ShouldNotBeNull();
            result.ShouldBe(resumes);
        }

        [Test]
        public async Task GetPagedResumesAsync_ValidParameters_ReturnsPagedResumes()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 10;
            var searchText = "Shoyeb";
            var orderBy = "FullName";

            var resumes = new List<Resume>
            {
                new Resume { Id = Guid.NewGuid(), FullName = "Shoyeb" },
                new Resume { Id = Guid.NewGuid(), FullName = "Shoyeb2" },
                new Resume { Id = Guid.NewGuid(), FullName = "Shoyeb3" }
            };

            var totalRecords = resumes.Count;

            _applicationUnitOfWork.Setup(x => x.Resumes)
                .Returns(_resumeRepositoryMock.Object);

            _resumeRepositoryMock.Setup(x => x.GetTableDataAsync(
                It.IsAny<Expression<Func<Resume, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .ReturnsAsync((resumes, totalRecords, resumes.Count))
                .Verifiable();

            // Act
            var result = await _resumeService.GetPagedResumesAsync(pageIndex, pageSize, searchText, orderBy);

            // Assert
            _applicationUnitOfWork.Verify(x => x.Resumes, Times.Once);
            _resumeRepositoryMock.Verify(x => x.GetTableDataAsync(
                It.IsAny<Expression<Func<Resume, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()), Times.Once);

            result.records.ShouldNotBeNull();
            result.records.ShouldBeEquivalentTo(resumes);
            result.total.ShouldBe(totalRecords);
            result.totalDisplay.ShouldBe(resumes.Count);
        }

        [Test]
        public async Task UpdateResume_ExistingResume_UpdatesResume()
        {
            // Arrange
            Resume resume = new Resume();

            _applicationUnitOfWork.Setup(x => x.Resumes)
                .Returns(_resumeRepositoryMock.Object);

            _resumeRepositoryMock.Setup(x => x.Edit(resume))
                .Verifiable();

            _applicationUnitOfWork.Setup(x => x.SaveAsync())
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            await _resumeService.UpdateResume(resume);

            // Assert
            _resumeRepositoryMock.VerifyAll();
            _applicationUnitOfWork.VerifyAll();
        }

        [Test]
        public async Task GetTemplateNameAsync_ExistingTemplate_ReturnsTemplateName()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            ResumeTemplate template = new ResumeTemplate { Id = id, Name = "Modern" };

            _applicationUnitOfWork.Setup(x => x.ResumeTemplates)
                .Returns(_resumeTemplateRepositoryMock.Object);

            _resumeTemplateRepositoryMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(template);

            // Act
            var result = await _resumeService.GetTemplateNameAsync(id);

            // Assert
            _resumeTemplateRepositoryMock.Verify(x => x.GetByIdAsync(id), Times.Once);

            result.ShouldNotBeNull();
            result.ShouldBe(template.Name);
        }

        [Test]
        public void GetResumesByUserId_ExistingUser_ReturnsListOfResumes()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            List<(Guid Id, string Title)> resumes = new List<(Guid Id, string Title)>
            {
                (Guid.NewGuid(), "Shoyeb's Resume 1"),
                (Guid.NewGuid(), "Shoyeb's Resume 2"),
                (Guid.NewGuid(), "Shoyeb's Resume 3")
            };

            _applicationUnitOfWork.Setup(x => x.Resumes)
                .Returns(_resumeRepositoryMock.Object);

            _resumeRepositoryMock.Setup(x => x.GetResumeByUserId(userId))
                .Returns(resumes);

            // Act
            var result = _resumeService.GetResumesByUserId(userId);

            // Assert
            _resumeRepositoryMock.Verify(x => x.GetResumeByUserId(userId), Times.Once);

            result.ShouldNotBeNull();
            result.ShouldBe(resumes);
        }

        [Test]
        public async Task GetPagedUsersAsync_ValidParameters_ReturnsPagedUsers()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 10;
            var searchText = "Shoyeb";
            var orderBy = "FullName";

            var outParameters = new Dictionary<string, Type>()
            {
                { "Total", typeof(int) },
                { "TotalDisplay", typeof(int) }
            };

            var users = new List<UserDTO>
            {
                new UserDTO { Id = Guid.NewGuid(), FullName = "Shoyeb" },
                new UserDTO { Id = Guid.NewGuid(), FullName = "Shoyeb2" },
                new UserDTO { Id = Guid.NewGuid(), FullName = "Shoyeb3" }
            };

            var totalRecords = users.Count;

            _adoNetUtility.Setup(x => x.QueryWithStoredProcedureAsync<UserDTO>(
                "GetPagedUserData",
                It.IsAny<Dictionary<string, object>>(),
                It.IsAny<Dictionary<string, Type>>()))
                .ReturnsAsync((users, new Dictionary<string, object>
                {
                { "Total", totalRecords },
                { "TotalDisplay", totalRecords }
                }));

            // Act
            var result = await _userService.GetPagedUsersAsync(pageIndex, pageSize, searchText, orderBy);

            // Assert
            _adoNetUtility.VerifyAll();

            result.records.ShouldNotBeNull();
            result.records.ShouldBeEquivalentTo(users);
            result.total.ShouldBe(3);
            result.totalDisplay.ShouldBe(3);
        }

        [Test]
        public void GetUser_ExistingUser_ReturnsUser()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            UserProfile user = new UserProfile { Id = id };

            _applicationUnitOfWork.Setup(x => x.UserProfiles)
                .Returns(_userRepositoryMock.Object);

            _userRepositoryMock.Setup(x => x.GetById(id))
                .Returns(user);

            // Act
            var result = _userService.GetUser(id);

            // Assert
            _userRepositoryMock.Verify(x => x.GetById(id), Times.Once);

            result.ShouldNotBeNull();
            result.ShouldBe(user);
        }

        [Test]
        public void UpdateUser_ExistingUser_UpdatesUser()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string address = "Puran Dhaka";
            string education = "Bachelor's Degree";
            string experience = "2 years";
            string imageUrl = "shoyeb.jpg";
            string designation = "Software Engineer";
            string githubUsername = "msashoyeb";
            string linkedInUsername = "msashoyeb";
            bool isActive = true;

            UserProfile userProfile = new UserProfile { Id = id };

            _applicationUnitOfWork.Setup(x => x.UserProfiles)
                .Returns(_userRepositoryMock.Object);

            _userRepositoryMock.Setup(x => x.GetById(id))
                .Returns(userProfile);

            _applicationUnitOfWork.Setup(x => x.Save())
                .Verifiable();

            // Act
            _userService.UpdateUser(id, address, education, experience, imageUrl, designation, githubUsername, linkedInUsername, isActive);

            // Assert
            _userRepositoryMock.Verify(x => x.GetById(id), Times.Once);
            _applicationUnitOfWork.Verify(x => x.Save(), Times.Once);

            userProfile.Address.ShouldBe(address);
            userProfile.Education.ShouldBe(education);
            userProfile.Experience.ShouldBe(experience);
            userProfile.ImageUrl.ShouldBe(imageUrl);
            userProfile.Designation.ShouldBe(designation);
            userProfile.GithubUsername.ShouldBe(githubUsername);
            userProfile.LinkedInUsername.ShouldBe(linkedInUsername);
            userProfile.IsActive.ShouldBe(isActive);
        }
    }
}