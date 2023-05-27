using AutoMapper;
using BysinessServices.Interfaces;
using BysinessServices.Models;
using DataAccess.Entities;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.Services
{
    public class UserService: Crud<UserWithAuthInfoModel, User>, IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<AdditionalRole> _roleRepository;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _userRepository = _unitOfWork.GetRepository<User>();
            _roleRepository = _unitOfWork.GetRepository<AdditionalRole>();
        }

        public override async Task<IEnumerable<UserWithAuthInfoModel>> GetAllAsync(params Expression<Func<User, object>>[] includes)
        {
            var resources = await _userRepository.GetAllAsync(u => u.Role);
            return _mapper.Map<IEnumerable<UserWithAuthInfoModel>>(resources);
        }

        public async Task<UserProtectedModel> AddProtectedAsync(UserWithAuthInfoModel userModel)
        {
            var userEntity = _mapper.Map<User>(userModel);
            await _userRepository.AddAsync(userEntity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<UserProtectedModel>(userEntity);
        }


        public async Task AddRoleAsync(RoleModel newRole)
        {
            await _roleRepository.AddAsync(_mapper.Map<AdditionalRole>(newRole));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateRoleAsync(RoleModel role)
        {
            _roleRepository.Update(_mapper.Map<AdditionalRole>(role));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            _roleRepository.DeleteByIdAsync(roleId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<RoleModel>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleModel>>(roles);
        }

        public async Task UpdateUserRole(int userId, RoleModel newRole)
        {
            var user = await _userRepository.GetByIdAsync(userId, u => u.Role);
            user.Role = _mapper.Map<AdditionalRole>(newRole);
            _userRepository.Update(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<UserProtectedModel>> GetAllUserWithoutProtectedInfo()
        {
            var usersWithoutProtectedInfo = await _userRepository.GetAllAsync(u => u.Role);
            return _mapper.Map<IEnumerable<UserProtectedModel>>(usersWithoutProtectedInfo);
        }

        public async Task<UserProtectedModel> GetUserWithoutProtectedInfoById(int id)
        {
            var userWithoutProtectedInfo = await _userRepository.GetByIdAsync(id, u => u.Role);
            return _mapper.Map<UserProtectedModel>(userWithoutProtectedInfo);
        }

        public UserWithAuthInfoModel ConvertToUserWithAuth(UserUnsafeModel unsafeUser, byte[] passwordHash, byte[] passwordSalt)
        {
            return new UserWithAuthInfoModel()
            {
                Id = unsafeUser.Id,
                FirstName = unsafeUser.FirstName,
                SecondName = unsafeUser.SecondName,
                LastName = unsafeUser.LastName,
                RoleId = unsafeUser.RoleId,
                RoleName = unsafeUser.RoleName,
                Requests = unsafeUser.Requests,
                Schedules = unsafeUser.Schedules,
                Login = unsafeUser.Login,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                JwtRefreshToken = "aaa"
            };
        }

        public async Task<int?> GetUserIdByLogin(string login)
        {
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Login == login);
            return user is not null ? user.Id : null;
        }

        /*
        public override async Task<UserWithAuthInfoModel> GetByIdAsync(int id)
        {
            var entitie = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserWithAuthInfoModel>(entitie);
        }
        /**/
    }
}
