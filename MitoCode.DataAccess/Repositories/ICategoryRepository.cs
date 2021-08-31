using System.Collections.Generic;
using System.Threading.Tasks;
using mitocode.netfullstack.entities;

namespace mitocode.netfullstack.dataaccess.Repositories
{
    public interface ICategoryRepository
    {
        Task<(ICollection<Category> collection, int total)> GetCollectionAsync(string filter, int page, int rows);
        Task<Category> GetItemAsync(int id);
        Task<int> CreateAsync(Category entity);
        Task UpdateAsync(Category entity);
        Task DeleteAsync(int id);
    }
}