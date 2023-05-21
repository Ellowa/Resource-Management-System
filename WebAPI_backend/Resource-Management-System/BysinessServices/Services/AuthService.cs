using AutoMapper;
using BysinessServices.Interfaces;
using BysinessServices.Models;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace BysinessServices.Services
{
    public class AuthService : IAuthService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IConfiguration _config;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = _unitOfWork.GetRepository<User>();
            _config = configuration;
        }

        public async Task AddRefreshTokenToUserByIdAsync(int userId, string refreshToken)
        {
            var user = await _userRepository.GetByIdAsync(userId, u => u.Role);
            user.JwtRefreshToken = refreshToken;
            _userRepository.Update(user);
        }

        public async Task<bool> VerifyPasswordHash(int userId, string password)
        {
            var user = await _userRepository.GetByIdAsync(userId, u => u.Role);
            using (var hmac = new HMACSHA256(user.PasswordSalt)) 
            {
                byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(user.PasswordHash);
            }
        }

        public string GenerateJwtAccessToken(UserWithAuthInfoModel user, TimeSpan expiretionTime)
        {
            //"JwtSettings:AccessTokenKey"
            return GenerateJwtToken(user, expiretionTime, System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("JwtSettings:AccessTokenKey").Value));
        }

        public string GenerateJwtRefreshToken(UserWithAuthInfoModel user, TimeSpan expiretionTime)
        {
            //"JwtSettings:RefreshTokenKey"
            return GenerateJwtToken(user, expiretionTime, System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("JwtSettings:RefreshTokenKey").Value));
        }

        private string GenerateJwtToken(UserWithAuthInfoModel user, TimeSpan expiretionTime, byte[] securityKey) 
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("nameidentifier", user.Id.ToString()),
                new Claim("name", user.Login),
                new Claim("role", user.RoleName)
            };

            var key = new SymmetricSecurityKey(securityKey);

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow + expiretionTime,
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] salt)
        {
            using (var hmac = new HMACSHA256()) 
            {
                salt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task RemoveRefreshTokenFromUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId, u => u.Role);
            user.JwtRefreshToken = "";
            _userRepository.Update(user);
        }
    }
}
