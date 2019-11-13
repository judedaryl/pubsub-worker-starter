using System.Collections.Generic;
using System.Threading.Tasks;

namespace PubSubWorkerStarter
{
    public interface IBaseManager<TModel> where TModel : class
    {
        Task<TModel> GetAsync(long id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<bool> DeleteAsync(long id);
        Task<long> CreateAsync(TModel model);
        Task<bool> UpdateAsync(long id, TModel model);
        Task<bool> ExistAsync(long id);
    }
}
