using BysinessServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.Interfaces
{
    public interface IRequestService : ICrud<RequestModel>
    {
        Task<IEnumerable<RequestModel>> GetByUserId(int userId);
        Task ConfirmRequest(RequestModel request);
    }
}
