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
    public class SkillServiceTests
    {
        private AutoMock _mock;
        private Mock<IApplicationUnitOfWork> _applicationUnitOfWork;
        private Mock<ISkillRepository> _skillRepositoryMock;
        private ISkillService _skillService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _applicationUnitOfWork = _mock.Mock<IApplicationUnitOfWork>();
            _skillRepositoryMock = _mock.Mock<ISkillRepository>();
            _skillService = _mock.Create<SkillService>();
        }

        [TearDown]
        public void Teardown()
        {
            _applicationUnitOfWork.Reset();
            _skillRepositoryMock.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public void CreateSkill_SkillDoesNotExists_CreateSkill()
        {
            // Arrange
            const string name = "Laravel";
            Skill skill = new Skill
            {
                Name = name
            };
            _applicationUnitOfWork.Setup(x => x.Skills)
                .Returns(_skillRepositoryMock.Object);

            _skillRepositoryMock.Setup(x => x.IsDuplicateName(name, null))
                .Returns(false).Verifiable();

            _applicationUnitOfWork.Setup(x => x.Save()).Verifiable();

            _skillRepositoryMock.Setup(x => x.Add(
               It.Is<Skill>(y => y.Name == name)))
               .Verifiable();

            // Act
            _skillService.CreateSkill(name);

            // Assert
            this.ShouldSatisfyAllConditions(_applicationUnitOfWork.VerifyAll,
                _skillRepositoryMock.VerifyAll);

        }

        [Test]
        public void CreateSkill_Exist_ThrowsError()
        {
            // Arrange
            const string name = "PHP";
            Skill skill = new Skill
            {
                Name = name
            };
            _applicationUnitOfWork.Setup(x => x.Skills)
                .Returns(_skillRepositoryMock.Object);

            _skillRepositoryMock.Setup(x => x.IsDuplicateName(name, null))
                .Returns(true);

            // Act
            Should.Throw<DuplicateNameException>(
                () => _skillService.CreateSkill(name));
        }
        [Test]
        public void GetSkill_ValidId_ReturnSkill()
        {
            // Arrange
            var id = 1;
            Skill skill = new Skill
            {
                Id = id
            };
            _applicationUnitOfWork.Setup(x => x.Skills)
                .Returns(_skillRepositoryMock.Object);

            _skillRepositoryMock.Setup(x => x.GetById(id))
                .Returns(skill);

            // Act
            var result = _skillService.GetSkill(id);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.Id.ShouldBe(id)
                );
        }

        [Test]
        public void DeleteSkill_SkillExists_DeletesSkill()
        {
            // Arrange
            var id = 1;
            Skill skill = new Skill
            {
                Id = id
            };

            _applicationUnitOfWork.Setup(x => x.Skills)
                .Returns(_skillRepositoryMock.Object);

            _applicationUnitOfWork.Setup(x => x.Save()).Verifiable();

            _skillRepositoryMock.Setup(x => x.Remove(id)).Verifiable();

            // Act
            _skillService.DeleteSkill(id);

            // Assert
            this.ShouldSatisfyAllConditions(_applicationUnitOfWork.VerifyAll,
                _skillRepositoryMock.VerifyAll);
        }


        [Test]
        public async Task GetPagedSkillsAsync_ValidParameters_ReturnsPagedSkills()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 10;
            var searchText = "C#";
            var orderBy = "Name";

            var skills = new List<Skill>
            {
                new Skill { Id = 1, Name = "C#" },
                new Skill { Id = 2, Name = "Java" },
                new Skill { Id = 3, Name = "Python" }
            };

            var totalRecords = skills.Count;

            _applicationUnitOfWork.Setup(x => x.Skills)
                .Returns(_skillRepositoryMock.Object);

            _skillRepositoryMock.Setup(x => x.GetTableDataAsync(
                It.IsAny<Expression<Func<Skill, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .ReturnsAsync((skills, totalRecords, skills.Count))
                .Verifiable();

            // Act
            var result = await _skillService.GetPagedSkillsAsync(pageIndex, pageSize, searchText, orderBy);

            // Assert
            _applicationUnitOfWork.Verify(x => x.Skills, Times.Once);
            _skillRepositoryMock.Verify(x => x.GetTableDataAsync(
                It.IsAny<Expression<Func<Skill, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()), Times.Once);

            result.records.ShouldNotBeNull();
            result.records.ShouldBeEquivalentTo(skills);
            result.total.ShouldBe(totalRecords);
            result.totalDisplay.ShouldBe(skills.Count);
        }


        [Test]
        public void GetSkill_SkillExists_ReturnSkill()
        {
            // Arrange
            var id = 1;
            Skill skill = new Skill
            {
                Id = id
            };

            _skillRepositoryMock.Setup(x => x.GetById(id))
                .Returns(skill);

            _applicationUnitOfWork.Setup(x => x.Skills)
                .Returns(_skillRepositoryMock.Object);

            // Act
            var result = _skillService.GetSkill(id);


            // Assert
            _skillRepositoryMock.Verify(x => x.GetById(id), Times.Once);
            // Verify that the result matches the  expected Skill
            result.ShouldNotBeNull();
            result.Id.ShouldBe(id);
            result.Name.ShouldBe(skill.Name);
        }

        [Test]
        public void GetSkills_SkillExist_ReturnListOfSkills()
        {
            // Arrange
            List<Skill> skills = new List<Skill>
            {
                new Skill
                {
                    Id = 1,
                    Name = "C++"
                },
                new Skill
                {
                    Id = 2,
                    Name = "Java"
                },
                new Skill
                {
                    Id = 3,
                    Name = "C"
                }
            };

            _skillRepositoryMock.Setup(x => x.GetAll())
                .Returns(skills);

            _applicationUnitOfWork.Setup(x => x.Skills)
                .Returns(_skillRepositoryMock.Object);

            // Act
            var result = _skillService.GetSkills();

            // Assert
            _skillRepositoryMock.Verify(x => x.GetAll(), Times.Once);

            // Verify that the result is not null and contains the same skills as expected
            result.ShouldNotBeNull();
            result.ShouldBeEquivalentTo(skills);
        }

        [Test]
        public void UpdateSkill_SkillExists_UpdatesSkill()
        {
            // Arrange
            var id = 1;
            string name = "C#";

            _skillRepositoryMock.Setup(x => x.IsDuplicateName(name, id))
                .Returns(false);

            _skillRepositoryMock.Setup(x => x.GetById(id))
                .Returns(new Skill { Id = id });

            _applicationUnitOfWork.Setup(x => x.Skills)
                .Returns(_skillRepositoryMock.Object);

            _applicationUnitOfWork.Setup(x => x.Save()).Verifiable();

            // Act
            _skillService.UpdateSkill(id, name);

            // Assert
            _skillRepositoryMock.Verify(x => x.IsDuplicateName(name, id));
            _skillRepositoryMock.Verify(x => x.GetById(id));
            _applicationUnitOfWork.Verify(x => x.Save());
        }

        [Test]
        public void UpdateSkill_DuplicateName_ThrowsException()
        {
            // Arrange
            var id = 1;
            string name = "R";

            _skillRepositoryMock.Setup(x => x.IsDuplicateName(name, id))
                .Returns(true);

            _applicationUnitOfWork.Setup(x => x.Skills)
                .Returns(_skillRepositoryMock.Object);

            // Act and Assert
            Should.Throw<DuplicateNameException>(
                () => _skillService.UpdateSkill(id, name));
        }
    }
}