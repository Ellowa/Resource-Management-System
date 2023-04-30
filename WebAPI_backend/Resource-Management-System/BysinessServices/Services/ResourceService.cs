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
    public class ResourceService : IResourceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Resource> _resourceRepository;
        private readonly IGenericRepository<Schedule> _scheduleRepository;
        private readonly IGenericRepository<ResourceType> _resourceTypeRepository;

        public ResourceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _resourceRepository = unitOfWork.GetRepository<Resource>();
            _scheduleRepository = unitOfWork.GetRepository<Schedule>();
            _resourceTypeRepository = unitOfWork.GetRepository<ResourceType>();
        }

        public async Task<ResourceModel> AddAsync(ResourceModel model)
        {
            var resource = _mapper.Map<Resource>(model);
            await _resourceRepository.AddAsync(resource);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ResourceModel>(resource);
        }

        public async Task AddResourceTypeAsync(ResourceTypeModel resourceTypeModel)
        {
            await _resourceTypeRepository.AddAsync(_mapper.Map<ResourceType>(resourceTypeModel));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await _resourceRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ResourceModel>> GetAllAsync()
        {
            var resources = await _resourceRepository.GetAllAsync(r => r.ResourceType);
            return _mapper.Map<IEnumerable<ResourceModel>>(resources);
        }

        public async Task<IEnumerable<ResourceTypeModel>> GetAllResourceTypesAsync()
        {
            var resourceTypess = await _resourceTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ResourceTypeModel>>(resourceTypess);
        }

        public async Task<ResourceModel> GetByIdAsync(int id)
        {
            var resource = await _resourceRepository.GetByIdAsync(id);
            return _mapper.Map<ResourceModel>(resource);
        }

        public async Task<IEnumerable<ScheduleModel>> GetScheduleByResourceId(int resourceId)
        {
            var schedules = await _scheduleRepository.GetAllAsync();
            var schedulesResourceId = schedules.Where(s=> s.ResourceId == resourceId);
            return _mapper.Map<IEnumerable<ScheduleModel>>(schedulesResourceId);
        }

        public async Task<IEnumerable<ScheduleModel>> GetScheduleByUserId(int userId)
        {
            var schedules = await _scheduleRepository.GetAllAsync();
            var schedulesUserId = schedules.Where(s => s.UserId == userId);
            return _mapper.Map<IEnumerable<ScheduleModel>>(schedulesUserId);
        }

        public async Task RemoveResourceTypeAsync(int resourceTypeId)
        {
            await _resourceTypeRepository.DeleteByIdAsync(resourceTypeId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(ResourceModel model)
        {
            _resourceRepository.Update(_mapper.Map<Resource>(model));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateResourceTypeAsync(ResourceTypeModel resourceTypeModel)
        {
            _resourceTypeRepository.Update(_mapper.Map<ResourceType>(resourceTypeModel));
            await _unitOfWork.SaveAsync();
        }
    }
}
