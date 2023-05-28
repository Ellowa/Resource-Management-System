using AutoMapper;
using BysinessServices.Models;
using BysinessServices.Services;
using BysinessServices;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Moq;
using FluentAssertions;

namespace Resource_Management_System.Tests.BusinessTests
{
    [TestFixture]
    public class RequestServiceTests
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
        public async Task RequestServiceTest_GetByUserId_ShouldReturnRequestModelsByUserId(int userId)
        {
            // Arrange
            var requests = new List<Request>()
            {
                new Request { Id = 1, UserId = 1},
                new Request { Id = 2, UserId = 1},
                new Request { Id = 3, UserId = 2}
            };

            var expected = _mapper.Map<IEnumerable<RequestModel>>(requests.Where(s => s.UserId == userId));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.GetRepository<Request>().GetAllAsync(s => s.Resource)).ReturnsAsync(requests.AsQueryable());

            var requestService = new RequestService(unitOfWorkMock.Object, _mapper);

            // Act
            var actual = await requestService.GetByUserId(userId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task RequestServiceTest_GetByResourceId_ShouldReturnRequestModelsByResourceId(int resourceId)
        {
            // Arrange
            var requests = new List<Request>()
            {
                new Request { Id = 1, ResourceId = 1},
                new Request { Id = 2, ResourceId = 1},
                new Request { Id = 3, ResourceId = 2}
            };

            var expected = _mapper.Map<IEnumerable<RequestModel>>(requests.Where(r => r.ResourceId == resourceId));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.GetRepository<Request>().GetAllAsync(s => s.Resource)).ReturnsAsync(requests.AsQueryable());

            var requestService = new RequestService(unitOfWorkMock.Object, _mapper);

            // Act
            var actual = await requestService.GetByResourceId(resourceId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task RequestServiceTest_ConfirmRequest_ShouldConfirmRequest()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.GetRepository<Request>().DeleteByIdAsync(It.IsAny<int>()));
            unitOfWorkMock.Setup(u => u.GetRepository<Schedule>().AddAsync(It.IsAny<Schedule>()));

            var requestService = new RequestService(unitOfWorkMock.Object, _mapper);

            // Act
            await requestService.ConfirmRequest(new RequestModel() { Id = 1});

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<Request>().DeleteByIdAsync(1), Times.Once);
            unitOfWorkMock.Verify(u => u.GetRepository<Schedule>().AddAsync(It.Is<Schedule>(s => s.Id == 1)), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }
    }
}
