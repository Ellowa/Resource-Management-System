using BysinessServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.Interfaces
{
    public interface IUserService : ICrud<UserWithAuthInfoModel>
    {
        /// <summary>
        /// Get all user roles awailable in the system (Database)
        /// </summary>
        /// <returns>Enumerator for "list" of roles</returns>
        Task<IEnumerable<RoleModel>> GetAllRolesAsync();

        /// <summary>
        /// Add new user role to the "list"
        /// </summary>
        /// <param name="newRole">new role</param>
        /// <returns>async method status</returns>
        Task AddRoleAsync(RoleModel newRole);

        /// <summary>
        /// Update existing user in the list (by ID in the role instance)
        /// </summary>
        /// <param name="role">the updated role</param>
        /// <returns>async method status</returns>
        Task UpdateRoleAsync(RoleModel role);

        /// <summary>
        /// Delete existing user role (see Models.RoleModel) by ID in the "list"
        /// </summary>
        /// <param name="roleId">ID of user role that must be deleted</param>
        /// <returns>async method status</returns>
        Task DeleteRoleAsync(int roleId);

        /// <summary>
        /// Change current user role to the given one
        /// </summary>
        /// <param name="userId">ID opf user (see Models.UserModel or Models.UserWithoutAuthInfoModel)</param>
        /// <param name="newRole">new role for user</param>
        /// <returns>async method status</returns>
        Task UpdateUserRole(int userId, RoleModel newRole);

        /// <summary>
        /// Get a "list" of users without protected info, 
        /// such as password hash, salt, jwt-tokens, etc.
        /// </summary>
        /// <returns>Enumerator for "list" of roles</returns>
        Task<IEnumerable<UserProtectedModel>> GetAllUserWithoutProtectedInfo();

        Task<UserProtectedModel> GetUserWithoutProtectedInfoById(int id);

        UserWithAuthInfoModel ConvertToProtected(UserUnsafeModel unsafeUser, byte[] passwordHash, byte[] passwordSalt);
    }
}
