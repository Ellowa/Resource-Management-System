using BysinessServices.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.Interfaces
{
    public interface IRequestService : ICrud<RequestModel, Request>
    {
        Task<IEnumerable<RequestModel>> GetByUserId(int userId);

        Task<IEnumerable<RequestModel>> GetByResourceId(int resourceId);

        Task ConfirmRequest(RequestModel requestModel);
    }
}
