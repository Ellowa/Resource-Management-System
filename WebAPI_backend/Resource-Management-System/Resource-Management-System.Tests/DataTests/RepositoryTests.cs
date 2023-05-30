using DataAccess;
using DataAccess.Entities;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Resource_Management_System.Tests.DataTests
{
    [TestFixture]
    public class RepositoryTests
    {
        private ApplicationDbContext _context;
        private IGenericRepository<Resource> _repository;

        [SetUp]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(dbContextOptions);
            _context.ResourceTypes.Add(new ResourceType() { Id = 1, TypeName = "test" });
            _context.SaveChanges();

            _repository = new Repository<Resource>(_context);
        }

        [Test]
        public async Task RepositoryTest_AddAsync_ShouldAddEntityToDatabase()
        {
            // Arrange
            var entity = new Resource() 
            { 
                Name = "test",
                SerialNumber = Guid.NewGuid().ToString(),
                ResourceTypeId = 1
            };

            // Act
            await _repository.AddAsync(entity);
            _context.SaveChanges();

            // Assert
            Assert.IsTrue(entity.Id != 0);
            Assert.That(_context.Resources, Has.Member(entity));
        }

        [Test]
        public void RepositoryTest_Delete_ShouldRemoveEntityFromDatabase()
        {
            // Arrange
            var entity = new Resource()
            {
                Name = "test",
                SerialNumber = Guid.NewGuid().ToString(),
                ResourceTypeId = 1
            };
            _context.Resources.Add(entity);
            _context.SaveChanges();

            // Act
            _repository.Delete(entity);
            _context.SaveChanges();

            // Assert
            Assert.That(_context.Resources, Has.No.Member(entity));
        }

        [Test]
        public async Task RepositoryTest_DeleteByIdAsync_ShouldRemoveEntityByIdFromDatabase()
        {
            // Arrange
            var entity = new Resource 
            { 
                Id = 1,
                Name = "test",
                SerialNumber = Guid.NewGuid().ToString(),
                ResourceTypeId = 1
            };
            _context.Resources.Add(entity);
            _context.SaveChanges();

            // Act
            await _repository.DeleteByIdAsync(1);
            _context.SaveChanges();

            // Assert
            Assert.That(_context.Resources, Has.No.Member(entity));
        }

        [Test]
        public async Task RepositoryTest_GetAllAsync_ShouldReturnAllEntitiesFromDatabase()
        {
            // Arrange
            var entity1 = new Resource()
            {
                Name = "test",
                SerialNumber = Guid.NewGuid().ToString(),
                ResourceTypeId = 1
            };
            var entity2 = new Resource()
            {
                Name = "test2",
                SerialNumber = Guid.NewGuid().ToString(),
                ResourceTypeId = 1
            };
            _context.Resources.AddRange(entity1, entity2);
            _context.SaveChanges();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(_context.Resources, Has.Member(entity1));
            Assert.That(_context.Resources, Has.Member(entity2));
        }

        [Test]
        public async Task RepositoryTest_GetByIdAsync_ShouldReturnEntityByIdFromDatabase()
        {
            // Arrange
            var entity = new Resource
            {
                Id = 1,
                Name = "test",
                SerialNumber = Guid.NewGuid().ToString(),
                ResourceTypeId = 1
            };
            _context.Resources.Add(entity);
            _context.SaveChanges();

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.That(result, Is.EqualTo(entity));
        }

        [Test]
        public void RepositoryTest_Update_ShouldMarkEntityAsModifiedInContext()
        {
            // Arrange
            var entity = new Resource();

            // Act
            _repository.Update(entity);

            // Assert
            var entry = _context.Entry(entity);
            Assert.That(entry.State, Is.EqualTo(EntityState.Modified));
        }

        [TearDown]
        public void Teardown()
        {
            _context.Dispose();
        }

    }
}
