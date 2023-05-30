using BysinessServices.Models;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                    PasswordHash = new byte[] { 56, 243, 29, 69, 152, 46, 44, 78, 191, 143, 
                        53, 91, 25, 59, 167, 150, 177, 210, 250, 195, 67, 43, 187, 180, 81, 88, 252, 220, 207, 101, 210, 92 },
                    PasswordSalt = new byte[] { 39, 146, 114, 52, 157, 243, 116, 83, 111, 
                        100, 17, 234, 231, 139, 241, 106, 61, 122, 227, 48, 63, 128, 218,
                        220, 240, 118, 113, 166, 243, 190, 81, 53, 19, 108, 43, 72, 227, 
                        173, 38, 167, 222, 10, 128, 86, 69, 96, 160, 47, 219, 185, 226, 228, 245, 71, 126, 166, 55 ,160, 114, 176, 55, 106, 163, 203 },
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
                    PasswordHash = new byte[] { 223, 240, 180, 123, 131, 5, 106, 50, 162, 56, 138, 101,
                        144, 149, 46, 112, 170, 16, 60, 33, 93, 154, 35, 201, 82, 63, 109, 154, 84, 140, 244, 204},
                    PasswordSalt = new byte[] { 139, 99, 40, 186, 180, 17, 110, 31, 13, 168, 23, 201,
                        234, 119, 83, 183, 125, 171, 212, 253, 204, 174, 206, 100, 118, 119, 229, 132,
                        86, 233, 216, 241, 248, 98, 23, 206, 50, 180, 6, 98, 73, 51, 29, 118, 10, 218,
                        80, 3, 252, 96, 57, 60, 236, 245, 98, 166, 7, 8, 212, 175, 39, 17, 239, 33 },
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
                    PasswordHash = new byte[] { 212, 106, 76, 246, 108, 78, 3, 125, 182, 22, 6, 58, 174,
                        232, 50, 73, 23, 60, 169, 191, 73, 66, 253, 247, 203, 68, 47, 123, 171, 247, 157, 48 },
                    PasswordSalt = new byte[] { 198, 44, 92, 215, 65, 214, 110, 29, 144, 71, 171, 145,
                        61, 97, 201, 61, 129, 124, 192, 221, 182, 211, 234, 158, 194, 56, 187, 178, 15,
                        221, 201, 33, 30, 130, 193, 223, 143, 237, 77, 45, 29, 87, 57, 23, 56, 210, 243,
                        206, 7, 120, 79, 118, 240, 231, 115, 115, 158, 227, 86, 119, 107, 197, 109, 2 },
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
