using System.Threading.Tasks;
using MitoCode.Dto.Request;
using MitoCode.Dto.Response;
using mitocode.netfullstack.dto;

namespace mitocode.netfullstack.services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDtoResponse> GetCollectionAsync(BaseRequest request);
        Task<ResponseDto<ProductDtoSingleResponse>> GetProductAsync(int id);
        Task<ResponseDto<int>> CreateAsync(ProductDtoRequest request);
        Task<ResponseDto<int>> UpdateAsync(int id, ProductDtoRequest request);
        Task<ResponseDto<int>> DeleteAsync(int id);
    }
}