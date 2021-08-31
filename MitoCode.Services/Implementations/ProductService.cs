using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MitoCode.Dto.Request;
using MitoCode.Dto.Response;
using mitocode.netfullstack.dataaccess.Repositories;
using mitocode.netfullstack.dto;
using mitocode.netfullstack.entities;
using mitocode.netfullstack.services.Interfaces;

namespace mitocode.netfullstack.services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<IProductRepository> _logger;

        public ProductService(IProductRepository repository, ILogger<IProductRepository> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ProductDtoResponse> GetCollectionAsync(BaseRequest request)
        {
            var response = new ProductDtoResponse();

            var tupla = await _repository.GetCollectionAsync(request.Filter ?? string.Empty, request.Page, request.Rows);

            response.Collection = tupla.collection
                .Select(x => new ProductDto
                {
                    ProductId = x.Id,
                    Category = x.CategoryName,
                    ProductName = x.Name,
                    UnitPrice = x.UnitPrice,
                })
                .ToList();

            response.TotalPages = MitoCodeUtils.GetTotalPages(tupla.total, request.Rows);

            return response;
        }

        public async Task<ResponseDto<ProductDtoSingleResponse>> GetProductAsync(int id)
        {
            var response = new ResponseDto<ProductDtoSingleResponse>();
            try
            {
                var product = await _repository.GetItemAsync(id);

                if (product == null)
                {
                    response.Success = false;
                    return response;
                }

                response.Result = new ProductDtoSingleResponse
                {
                    ProductId = product.Id,
                    ProductName = product.ProductName,
                    CategoryId = product.CategoryId,
                    ProductPrice = product.UnitPrice,
                    ProductEnabled = product.Enabled,
                };

                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
            }

            return response;
        }

        public async Task<ResponseDto<int>> CreateAsync(ProductDtoRequest request)
        {
            var response = new ResponseDto<int>();

            try
            {
                var result = await _repository.CreateAsync(new Product
                {
                    ProductName = request.ProductName,
                    CategoryId = request.CategoryId,
                    UnitPrice = request.ProductPrice,
                    Enabled = request.ProductEnabled,
                });

                response.Result = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
            }

            return response;
        }

        public async Task<ResponseDto<int>> UpdateAsync(int id, ProductDtoRequest request)
        {
            var response = new ResponseDto<int>();

            try
            {
                await _repository.UpdateAsync(new Product
                {
                    Id = id,
                    ProductName = request.ProductName,
                    CategoryId = request.CategoryId,
                    UnitPrice = request.ProductPrice,
                    Enabled = request.ProductEnabled,
                });

                response.Result = id;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
            }

            return response;
        }

        public async Task<ResponseDto<int>> DeleteAsync(int id)
        {
            var response = new ResponseDto<int>();
            try
            {
                await _repository.DeleteAsync(id);
                response.Result = id;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
            }

            return response;
        }
    }
}