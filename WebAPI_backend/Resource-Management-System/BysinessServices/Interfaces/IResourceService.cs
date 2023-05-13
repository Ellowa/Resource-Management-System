using BysinessServices.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.Interfaces
{
    public interface IResourceService : ICrud<ResourceModel, Resource>
    {
        Task<IEnumerable<ScheduleModel>> GetScheduleByResourceId(int resourceId);
        Task<IEnumerable<ScheduleModel>> GetScheduleByUserId(int userId);

        Task<IEnumerable<ResourceTypeModel>> GetAllResourceTypesAsync(params Expression<Func<ResourceType, object>>[] includes);
        Task<ResourceTypeModel> AddResourceTypeAsync(ResourceTypeModel resourceTypeModel);
        Task UpdateResourceTypeAsync(ResourceTypeModel resourceTypeModel);
        Task RemoveResourceTypeAsync(int resourceTypeId);
    }
}
