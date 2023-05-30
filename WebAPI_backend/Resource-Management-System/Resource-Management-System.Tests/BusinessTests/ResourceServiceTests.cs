using AutoMapper;
using BysinessServices;
using BysinessServices.Models;
using BysinessServices.Services;
using DataAccess.Entities;
using DataAccess.Interfaces;
using FluentAssertions;
using Moq;

namespace Resource_Management_System.Tests.BusinessTests
{
    [TestFixture]
    public class ResourceServiceTests
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task ResourceServiceTest_GetScheduleByResourceId_ShouldReturnScheduleModelsByResourceId(int resourceId)
        {
            // Arrange
            var schedules = new List<Schedule>()
            {
                new Schedule { Id = 1, ResourceId = 1},
                new Schedule { Id = 2, ResourceId = 1},
                new Schedule { Id = 3, ResourceId = 2}
            };

            var expected = _mapper.Map<IEnumerable<ScheduleModel>>(schedules.Where(s => s.ResourceId == resourceId));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.GetRepository<Schedule>().GetAllAsync(s => s.Resource)).ReturnsAsync(schedules.AsQueryable());

            var resourceService = new ResourceService(unitOfWorkMock.Object, _mapper);

            // Act
            var actual = await resourceService.GetScheduleByResourceId(resourceId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task ResourceServiceTest_GetScheduleByUserId_houldReturnScheduleModelsByUserId(int userId)
        {
            // Arrange
            var schedules = new List<Schedule>()
            {
                new Schedule { Id = 1, UserId = 1},
                new Schedule { Id = 2, UserId = 1},
                new Schedule { Id = 3, UserId = 2}
            };

            var expected = _mapper.Map<IEnumerable<ScheduleModel>>(schedules.Where(s => s.UserId == userId));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.GetRepository<Schedule>().GetAllAsync(s => s.Resource)).ReturnsAsync(schedules.AsQueryable());

            var resourceService = new ResourceService(unitOfWorkMock.Object, _mapper);

            // Act
            var actual = await resourceService.GetScheduleByUserId(userId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task ResourceServiceTest_GetAllResourceTypesAsync_ShouldReturnAllResourceTypesModels()
        {
            // Arrange
            var entities = new List<ResourceType>()
            {
                new ResourceType { Id = 1, TypeName = "test1"},
                new ResourceType { Id = 2, TypeName = "test2"}
            };

            var expected = _mapper.Map<IEnumerable<ResourceTypeModel>>(entities);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.GetRepository<ResourceType>().GetAllAsync()).ReturnsAsync(entities.AsQueryable());

            var resourceService = new ResourceService(unitOfWorkMock.Object, _mapper);

            // Act
            var actual = await resourceService.GetAllResourceTypesAsync();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task ResourceServiceTest_AddResourceTypeAsync_ShouldAddResourceTypeModel()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.GetRepository<ResourceType>().AddAsync(It.IsAny<ResourceType>()));

            var resourceService = new ResourceService(unitOfWorkMock.Object, _mapper);

            // Act
            await resourceService.AddResourceTypeAsync( new ResourceTypeModel() { Id = 1 });

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<ResourceType>().AddAsync(It.Is<ResourceType>(r => r.Id == 1)), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task ResourceServiceTest_UpdateResourceTypeAsync_ShouldUpdateResourceTypeModel()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.GetRepository<ResourceType>().Update(It.IsAny<ResourceType>()));

            var resourceService = new ResourceService(unitOfWorkMock.Object, _mapper);

            // Act
            await resourceService.UpdateResourceTypeAsync(new ResourceTypeModel() { Id = 1 });

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<ResourceType>().Update(It.Is<ResourceType>(r => r.Id == 1)), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task ResourceServiceTest_RemoveResourceTypeAsync_ShouldDeleteResourceTypeModel(int id)
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.GetRepository<ResourceType>().DeleteByIdAsync(It.IsAny<int>()));

            var resourceService = new ResourceService(unitOfWorkMock.Object, _mapper);

            // Act
            await resourceService.RemoveResourceTypeAsync(id);

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<ResourceType>().DeleteByIdAsync(id), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }
    }
}
