using AutoMapper;
using BysinessServices.Interfaces;
using BysinessServices.Models;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var user = await _userRepository.GetByIdAsync(userId);
            user.Role = _mapper.Map<AdditionalRole>(newRole);
            _userRepository.Update(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<UserModel>> GetAllUserWithoutProtectedInfo()
        {
            var usersWithoutProtectedInfo = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserModel>>(usersWithoutProtectedInfo);
        }
    }
}
