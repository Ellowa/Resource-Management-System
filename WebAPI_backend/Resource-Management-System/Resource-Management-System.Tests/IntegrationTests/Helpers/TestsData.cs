using BysinessServices.Models;
using DataAccess;
using DataAccess.Entities;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;

namespace Resource_Management_System.Tests.IntegrationTests.Helpers
{
    public class TestsData
    {
        public static void SeedData(ApplicationDbContext context)
        {
            context.AdditionalRoles.AddRange(
                new AdditionalRole { Id = 1, Name = "admin" },
                new AdditionalRole { Id = 2, Name = "manager" },
                new AdditionalRole { Id = 3, Name = "user" });
            context.Users.AddRange(
                new User
                {
                    Id = 1,
                    FirstName = "FirstName1",
                    SecondName = "SecondName1",
                    LastName = "LastName1",
                    Login = "Admin_test",
                    PasswordHash = new byte[] { 1 },
                    PasswordSalt = new byte[] { 1 },
                    RoleId = 1,
                    JwtRefreshToken = "JwtRefreshToken1"
                },
                new User
                {
                    Id = 2,
                    FirstName = "FirstName2",
                    SecondName = "SecondName2",
                    LastName = "LastName2",
                    Login = "Manager_test",
                    PasswordHash = new byte[] { 2 },
                    PasswordSalt = new byte[] { 2 },
                    RoleId = 2,
                    JwtRefreshToken = "JwtRefreshToken2"
                },
                new User
                {
                    Id = 3,
                    FirstName = "FirstName3",
                    SecondName = "SecondName3",
                    LastName = "LastName3",
                    Login = "User_test",
                    PasswordHash = new byte[] { 3 },
                    PasswordSalt = new byte[] { 3 },
                    RoleId = 3,
                    JwtRefreshToken = "JwtRefreshToken3"
                });

            context.ResourceTypes.AddRange(
                new ResourceType { Id = 1, TypeName = "type1" },
                new ResourceType { Id = 2, TypeName = "type2" },
                new ResourceType { Id = 3, TypeName = "type3" });
            context.Resources.AddRange(
                new Resource { Id = 1, Name = "ResourceName1", SerialNumber = "SerialNumber1", ResourceTypeId = 1 },
                new Resource { Id = 2, Name = "ResourceName2", SerialNumber = "SerialNumber2", ResourceTypeId = 1 },
                new Resource { Id = 3, Name = "ResourceName3", SerialNumber = "SerialNumber3", ResourceTypeId = 2 });

            context.Requests.AddRange(
                new Request { Id = 1, Purpose = "Purpose1", ResourceId = 1, UserId = 2 },
                new Request { Id = 2, Purpose = "Purpose2", ResourceId = 2, UserId = 2 },
                new Request { Id = 3, Purpose = "Purpose3", ResourceId = 1, UserId = 3 });
            context.Schedules.AddRange(
                new Schedule { Id = 1, Purpose = "Purpose1", ResourceId = 3, UserId = 3 },
                new Schedule { Id = 2, Purpose = "Purpose2", ResourceId = 2, UserId = 3 },
                new Schedule { Id = 3, Purpose = "Purpose3", ResourceId = 3, UserId = 1 });

            context.SaveChanges();
        }

        public static IEnumerable<ResourceTypeModel> GetTestResourceTypeModels =>
            new List<ResourceTypeModel>()
            {
                new ResourceTypeModel { Id = 1, TypeName = "type1" },
                new ResourceTypeModel { Id = 2, TypeName = "type2" },
                new ResourceTypeModel { Id = 3, TypeName = "type3" }
            };

        public static IEnumerable<ResourceTypeModel> GetTestResourceTypeModelsWithDetails =>
            new List<ResourceTypeModel>()
            {
                new ResourceTypeModel { Id = 1, TypeName = "type1",
                    Resources = new List<ResourceModel>
                    {
                        new ResourceModel { Id = 1, Name = "ResourceName1", SerialNumber = "SerialNumber1", ResourceTypeId = 1, ResourceTypeName = "type1" },
                        new ResourceModel { Id = 2, Name = "ResourceName2", SerialNumber = "SerialNumber2", ResourceTypeId = 1, ResourceTypeName = "type1" }
                    }},
                new ResourceTypeModel { Id = 2, TypeName = "type2",
                    Resources = new List<ResourceModel>
                    {
                        new ResourceModel { Id = 3, Name = "ResourceName3", SerialNumber = "SerialNumber3", ResourceTypeId = 2, ResourceTypeName = "type2" }
                    }},
                new ResourceTypeModel { Id = 3, TypeName = "type3" }
            };
    }
}
