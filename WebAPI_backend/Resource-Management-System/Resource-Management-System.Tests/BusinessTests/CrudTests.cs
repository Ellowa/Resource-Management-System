using AutoMapper;
using BysinessServices;
using BysinessServices.Interfaces;
using BysinessServices.Models;
using BysinessServices.Services;
using DataAccess.Entities;
using DataAccess.Interfaces;
using FluentAssertions;
using Moq;
using Org.BouncyCastle.Asn1;

namespace Resource_Management_System.Tests.BusinessTests
{
    [TestFixture]
    public class CrudTests
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup() 
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
        }

        [Test]
        public async Task CrudTest_GetAllAsync_ShouldReturnAllModels()
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

            var crud = new Crud<ResourceTypeModel, ResourceType>(unitOfWorkMock.Object, _mapper);

            // Act
            var actual = await crud.GetAllAsync();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task CrudTest_GetAllAsync_ShouldReturnAllModelsWithDetails()
        {
            // Arrange
            var entities = new List<Request>()
            {
                new Request { Id = 1, ResourceId = 1, UserId = 1, Purpose = "test1", Resource = new Resource(){ Id = 1, Name = "testName1" }, User = new User(){ Id = 1 } },
                new Request { Id = 2, ResourceId = 2, UserId = 2, Purpose = "test2", Resource = new Resource(){ Id = 2, Name = "testName2" }, User = new User(){ Id = 2 } }
            };

            var expected = _mapper.Map<IEnumerable<RequestModel>>(entities);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.GetRepository<Request>().GetAllAsync(r => r.Resource)).ReturnsAsync(entities.AsQueryable());

            var crud = new Crud<RequestModel, Request>(unitOfWorkMock.Object, _mapper);

            // Act
            var actual = await crud.GetAllAsync(r => r.Resource);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task CrudTest_GetByIdAsync_ShouldReturnModelsById(int id)
        {
            // Arrange
            var entities = new List<Request>()
            {
                new Request { Id = 1, ResourceId = 1, UserId = 1, Purpose = "test1" },
                new Request { Id = 2, ResourceId = 2, UserId = 2, Purpose = "test2" }
            };

            var expected = _mapper.Map<RequestModel>(entities.FirstOrDefault(e => e.Id == id));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.GetRepository<Request>().GetByIdAsync(It.IsAny<int>())).ReturnsAsync(entities.FirstOrDefault(e => e.Id == id));

            var crud = new Crud<RequestModel, Request>(unitOfWorkMock.Object, _mapper);

            // Act
            var actual = await crud.GetByIdAsync(id);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task CrudTest_AddAsync_ShouldAddModel()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.GetRepository<User>().AddAsync(It.IsAny<User>()));

            var crud = new Crud<UserWithAuthInfoModel, User>(unitOfWorkMock.Object, _mapper);

            // Act
            await crud.AddAsync(new UserWithAuthInfoModel());

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<User>().AddAsync(It.IsAny<User>()), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task CrudTest_UpdateAsync_ShouldUpdateModel()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.GetRepository<Resource>().Update(It.IsAny<Resource>()));

            var crud = new Crud<ResourceModel, Resource>(unitOfWorkMock.Object, _mapper);

            // Act
            await crud.UpdateAsync(new ResourceModel() { Id = 1});

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<Resource>().Update(It.Is<Resource>(r => r.Id == 1)), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task CrudTest_DeleteAsync_ShouldDeleteModel(int id)
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.GetRepository<Resource>().DeleteByIdAsync(It.IsAny<int>()));

            var crud = new Crud<ResourceModel, Resource>(unitOfWorkMock.Object, _mapper);

            // Act
            await crud.DeleteAsync(id);

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<Resource>().DeleteByIdAsync(id), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }
    }
}
