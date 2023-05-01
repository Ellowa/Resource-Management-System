using BysinessServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.Interfaces
{
    internal interface IAuthService
    {
        /// <summary>
        /// Add a new JWT refresh token to a user
        /// </summary>
        /// <param name="userId">user ID (see Models.UserModel or Models.UserWithoutAuthInfoModel)</param>
        /// <param name="refreshToken">string of JWT refresh token</param>
        /// <returns>async method status</returns>
        Task AddRefreshTokenToUserByIdAsync(int userId, string refreshToken);

        /// <summary>
        /// Remove JWT refresh token from user
        /// </summary>
        /// <param name="userId">user ID (see Models.UserModel or Models.UserWithoutAuthInfoModel)</param>
        /// <returns>async method status</returns>
        Task RemoveRefreshTokenFromUserByIdAsync(int userId);

        /// <summary>
        /// Compare that password (its hash) provided by user is correct
        /// </summary>
        /// <param name="userId">the user ID (see Models.UserModel or Models.UserWithoutAuthInfoModel)</param>
        /// <param name="password">the password provided by user</param>
        /// <returns>true - if password correct. false - if password incorrect</returns>
        Task<bool> VerifyPasswordHash(int userId, string password);

        /// <summary>
        /// Create hash for password using SHACSHA256 algorythm
        /// </summary>
        /// <param name="password">password that must be hashed</param>
        /// <param name="passwordHash">hash of password</param>
        /// <param name="salt">the cryptography salt used during hashing</param>
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] salt);


        /// <summary>
        /// Generate JWT access token for user
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="expiretionTime">time when the token expires</param>
        /// <returns>JWT access token</returns>
        string GenerateJwtAccessToken(UserWithAuthInfoModel user, TimeSpan expiretionTime);

        /// <summary>
        /// Generate JWT refresh token for user
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>JWT refresh token</returns>
        string GenerateJwtRefreshToken(UserWithAuthInfoModel user);
    }
}
