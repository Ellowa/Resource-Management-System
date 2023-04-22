using DataAccess.Entities;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        private IGenericRepository<AdditionalRole> _additionalRoleRepository;
        private IGenericRepository<Request> _requestRepository;
        private IGenericRepository<Resource> _resourceRepository;
        private IGenericRepository<ResourceType> _resourceTypeRepository;
        private IGenericRepository<Schedule> _scheduleRepository;
        private IGenericRepository<User> _userRepository;

        private bool _disposed = false;

        public UnitOfWork(ApplicationDbContext context) 
        {
            this._context = context;
            //Initialisation of repositories
            _additionalRoleRepository = new Repository<AdditionalRole>(_context);
            _requestRepository = new Repository<Request>(_context);
            _resourceRepository = new Repository<Resource>(_context);
            _resourceTypeRepository = new Repository<ResourceType>(_context);
            _scheduleRepository = new Repository<Schedule>(_context);
            _userRepository = new Repository<User>(_context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) 
        {
            if (!_disposed)
            {
                if (disposing) 
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        { 
            TEntity entity = null;
            switch (entity) {
                case AdditionalRole _: 
                    return _additionalRoleRepository as IGenericRepository<TEntity>;
                    break;
                case Request _: 
                    return _requestRepository as IGenericRepository<TEntity>;
                    break;
                case Resource _:
                    return _resourceRepository as IGenericRepository<TEntity>;
                    break;
                case ResourceType _:
                    return _resourceTypeRepository as IGenericRepository<TEntity>;
                    break;
                case Schedule _:
                    return _scheduleRepository as IGenericRepository<TEntity>;
                    break;
                case User _:
                    return _userRepository as IGenericRepository<TEntity>;
                    break;
                default: 
                    throw new NotImplementedException();
            }
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
