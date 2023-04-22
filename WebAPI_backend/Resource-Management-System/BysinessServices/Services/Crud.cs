﻿using AutoMapper;
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
    internal class Crud<TModel, TEntity> : ICrud<TModel> where TModel : class 
                                                         where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public Crud(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<TEntity>();
            _mapper = mapper;
        }

        public async Task AddAsync(TModel model)
        {
            await _repository.AddAsync(_mapper.Map<TEntity>(model));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await _repository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TModel>>(entities);
        }

        public async Task<TModel> GetByIdAsync(int id)
        {
            var entitie = await _repository.GetByIdAsync(id);
            return _mapper.Map<TModel>(entitie);
        }

        public async Task UpdateAsync(TModel model)
        {
            _repository.Update(_mapper.Map<TEntity>(model));
            await _unitOfWork.SaveAsync();
        }
    }
}