using System.Collections.Generic;
using System.Threading.Tasks;
using mitocode.netfullstack.entities;
using mitocode.netfullstack.entities.Complex;

namespace mitocode.netfullstack.dataaccess.Repositories
{
    public interface IProductRepository
    {
        Task<(ICollection<ProductInformation> collection, int total)> GetCollectionAsync(string filter, int page, int rows);
        Task<Product> GetItemAsync(int id);
        Task<int> CreateAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task DeleteAsync(int id);

    }
}