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
    public class ResourceService : Crud<ResourceModel, Resource>, IResourceService
    {
        private readonly IGenericRepository<Resource> _resourceRepository;
        private readonly IGenericRepository<Schedule> _scheduleRepository;
        private readonly IGenericRepository<ResourceType> _resourceTypeRepository;

        public ResourceService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _resourceRepository = unitOfWork.GetRepository<Resource>();
            _scheduleRepository = unitOfWork.GetRepository<Schedule>();
            _resourceTypeRepository = unitOfWork.GetRepository<ResourceType>();
        }

        public async Task<ResourceTypeModel> AddResourceTypeAsync(ResourceTypeModel resourceTypeModel)
        {
            var entity = _mapper.Map<ResourceType>(resourceTypeModel);
            await _resourceTypeRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ResourceTypeModel>(entity);
        }

        public async Task<IEnumerable<ResourceTypeModel>> GetAllResourceTypesAsync(params Expression<Func<ResourceType, object>>[] includes)
        {
            var resourceTypess = await _resourceTypeRepository.GetAllAsync(includes);
            return _mapper.Map<IEnumerable<ResourceTypeModel>>(resourceTypess);
        }

        public async Task<IEnumerable<ScheduleModel>> GetScheduleByResourceId(int resourceId)
        {
            var schedules = await _scheduleRepository.GetAllAsync(s => s.Resource);
            var schedulesResourceId = schedules.Where(s=> s.ResourceId == resourceId);
            return _mapper.Map<IEnumerable<ScheduleModel>>(schedulesResourceId);
        }

        public async Task<IEnumerable<ScheduleModel>> GetScheduleByUserId(int userId)
        {
            var schedules = await _scheduleRepository.GetAllAsync(s => s.Resource);
            var schedulesUserId = schedules.Where(s => s.UserId == userId);
            return _mapper.Map<IEnumerable<ScheduleModel>>(schedulesUserId);
        }

        public async Task RemoveResourceTypeAsync(int resourceTypeId)
        {
            await _resourceTypeRepository.DeleteByIdAsync(resourceTypeId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateResourceTypeAsync(ResourceTypeModel resourceTypeModel)
        {
            _resourceTypeRepository.Update(_mapper.Map<ResourceType>(resourceTypeModel));
            await _unitOfWork.SaveAsync();
        }
    }
}
