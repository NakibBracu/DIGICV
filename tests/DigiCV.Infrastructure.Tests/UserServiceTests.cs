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
    public class UserServiceTests
    {
        private AutoMock _mock;
        private Mock<IApplicationUnitOfWork> _applicationUnitOfWork;
        private Mock<IUserProfileRepository> _userRepositoryMock;
        private Mock<IAdoNetUtility> _adoNetUtility;
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
            _userRepositoryMock = _mock.Mock<IUserProfileRepository>();
            _adoNetUtility = _mock.Mock<IAdoNetUtility>();
            _userService = _mock.Create<UserService>();
        }

        [TearDown]
        public void Teardown()
        {
            _applicationUnitOfWork.Reset();
            _userRepositoryMock.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task GetPagedUsersAsync_ValidParameters_ReturnsPagedUsers()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 10;
            var searchText = "Shoyeb";
            var orderBy = "Name";

            var users = new List<UserDTO>
            {
                new UserDTO { Id = Guid.NewGuid(), FullName = "Shoyeb", ImageUrl = "shoyeb.png", ClaimType = "User", IsActive = true },
                new UserDTO { Id = Guid.NewGuid(), FullName = "Shoyeb2", ImageUrl = "shoyeb2.png", ClaimType = "User", IsActive = true },
                new UserDTO { Id = Guid.NewGuid(), FullName = "Shoyeb3", ImageUrl = "shoyeb3.png", ClaimType = "User", IsActive = true }
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
            _adoNetUtility.Verify(x => x.QueryWithStoredProcedureAsync<UserDTO>(
                "GetPagedUserData",
                It.Is<Dictionary<string, object>>(d =>
                    d["PageIndex"].Equals(pageIndex) &&
                    d["PageSize"].Equals(pageSize) &&
                    d["SearchText"].Equals(searchText) &&
                    d["OrderBy"].Equals(orderBy)),
                It.Is<Dictionary<string, Type>>(d =>
                    d.ContainsKey("Total") &&
                    d.ContainsKey("TotalDisplay"))),
                Times.Once);

            result.records.ShouldNotBeNull();
            result.records.ShouldBeEquivalentTo(users);
            result.total.ShouldBe(totalRecords);
            result.totalDisplay.ShouldBe(totalRecords);
        }

        [Test]
        public void GetUser_ValidId_ReturnsUser()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userProfile = new UserProfile { Id = id };

            _applicationUnitOfWork.Setup(x => x.UserProfiles.GetById(id))
                .Returns(userProfile);

            // Act
            var result = _userService.GetUser(id);

            // Assert
            _applicationUnitOfWork.Verify(x => x.UserProfiles.GetById(id), Times.Once);

            result.ShouldNotBeNull();
            result.Id.ShouldBe(id);
        }

        [Test]
        public void UpdateUser_ValidParameters_UpdatesUser()
        {
            // Arrange
            var id = Guid.NewGuid();
            var address = "Puran Dhaka";
            var education = "Bachelor's Degree";
            var experience = "2 years";
            var imageUrl = "shoyeb.jpg";
            var designation = "Software Engineer";
            var githubUsername = "msashoyeb";
            var linkedInUsername = "msashoyeb";
            var isActive = true;

            var userProfile = new UserProfile { Id = id };

            _applicationUnitOfWork.Setup(x => x.UserProfiles.GetById(id))
                .Returns(userProfile);
            _applicationUnitOfWork.Setup(x => x.Save()).Verifiable();

            // Act
            _userService.UpdateUser(id, address, education, experience, imageUrl, designation, githubUsername, linkedInUsername, isActive);

            // Assert
            _applicationUnitOfWork.Verify(x => x.UserProfiles.GetById(id), Times.Once);
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