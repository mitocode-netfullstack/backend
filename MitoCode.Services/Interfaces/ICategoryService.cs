using System.Threading.Tasks;
using MitoCode.Dto.Request;
using MitoCode.Dto.Response;
using mitocode.netfullstack.dto;

namespace mitocode.netfullstack.services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDtoResponse> GetCollectionAsync(BaseRequest request);
        
        Task<ResponseDto<CategoryDto>> GetAsync(int id);

        Task<ResponseDto<int>> CreateAsync(CategoryDtoRequest request);

        Task<ResponseDto<int>> UpdateAsync(int id, CategoryDtoRequest request);

        Task<ResponseDto<int>> DeleteAsync(int id);
    }
}