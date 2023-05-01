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
    public class Crud<TModel, TEntity> : ICrud<TModel> where TModel : class 
                                                         where TEntity : class, new()
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public Crud(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<TEntity>();
            _mapper = mapper;
        }

        public virtual async Task<TModel> AddAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task DeleteAsync(int modelId)
        {
            await _repository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TModel>>(entities);
        }

        public virtual async Task<TModel> GetByIdAsync(int id)
        {
            var entitie = await _repository.GetByIdAsync(id);
            return _mapper.Map<TModel>(entitie);
        }

        public virtual async Task UpdateAsync(TModel model)
        {
            _repository.Update(_mapper.Map<TEntity>(model));
            await _unitOfWork.SaveAsync();
        }
    }
}
