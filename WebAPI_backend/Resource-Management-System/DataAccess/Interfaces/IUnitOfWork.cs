using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAdditionalRoleRepository AdditionalRoleRepository { get; }

        IRequestRepository RequestRepository { get; }

        IResourceRepository ResourceRepository { get; }

        IResourceTypeRepository ResourceTypeRepository { get; }

        IScheduleRepository ScheduleRepository { get; }

        IUserRepository UserRepository { get; }

        Task SaveAsync();
    }
}
