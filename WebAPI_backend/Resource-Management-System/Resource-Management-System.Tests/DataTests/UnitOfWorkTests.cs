using DataAccess.Interfaces;
using DataAccess;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Resource_Management_System.Tests.DataTests
{
    [TestFixture]
    public class UnitOfWorkTests
    {

        [Test]
        public void UnitOfWorkTest_GetRepository_ReturnsRepositoryForEntity()
        {
            // Arrange
            var contextMock = new Mock<ApplicationDbContext>();
            var unitOfWork = new UnitOfWork(contextMock.Object);

            // Act
            var repository = unitOfWork.GetRepository<AdditionalRole>();

            // Assert
            Assert.That(repository, Is.Not.Null);
            Assert.That(repository, Is.InstanceOf<IGenericRepository<AdditionalRole>>());
        }

        [Test]
        public async Task UnitOfWorkTest_SaveAsync_SavesChanges()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(dbContextOptions);

            var unitOfWork = new UnitOfWork(context);
            var repository = new Repository<AdditionalRole>(context);

            var entity = new AdditionalRole()
            {
                Name = "test"
            };

            // Act
            await repository.AddAsync(entity);
            await unitOfWork.SaveAsync();

            // Assert
            Assert.IsTrue(entity.Id != 0);
            Assert.That(context.AdditionalRoles, Has.Member(entity));
        }
    }
}
