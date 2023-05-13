using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.Interfaces
{
    public interface ICrud<TModel, TEntity> where TModel : class
                                            where TEntity : class, new()
    {

        Task<IEnumerable<TModel>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);

        Task<TModel> GetByIdAsync(int id);

        Task<TModel> AddAsync(TModel model);

        Task UpdateAsync(TModel model);

        Task DeleteAsync(int modelId);
    }
}
