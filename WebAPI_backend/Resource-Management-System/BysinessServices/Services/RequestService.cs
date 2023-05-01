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
    public class RequestService : Crud<RequestModel, Request>, IRequestService
    {
        private readonly IGenericRepository<Request> _requestRepository;
        private readonly IGenericRepository<Schedule> _scheduleRepository;

        public RequestService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _requestRepository = unitOfWork.GetRepository<Request>();
            _scheduleRepository = unitOfWork.GetRepository<Schedule>();
        }

        public async Task ConfirmRequest(RequestModel requestModel)
        {
            var request = _mapper.Map<Schedule>(requestModel);
            await _scheduleRepository.AddAsync(request);
            await _requestRepository.DeleteByIdAsync(request.Id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<RequestModel>> GetByResourceId(int resourceId)
        {
            var requests = await _requestRepository.GetAllAsync();
            var requestsResourceId = requests.Where(s => s.ResourceId == resourceId);
            return _mapper.Map<IEnumerable<RequestModel>>(requestsResourceId);
        }

        public async Task<IEnumerable<RequestModel>> GetByUserId(int userId)
        {
            var requests = await _requestRepository.GetAllAsync();
            var requestsUserId = requests.Where(s => s.UserId == userId);
            return _mapper.Map<IEnumerable<RequestModel>>(requestsUserId);
        }
    }
}
