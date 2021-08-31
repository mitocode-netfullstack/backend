using Microsoft.Extensions.DependencyInjection;
using mitocode.netfullstack.dataaccess.Repositories;
using mitocode.netfullstack.services.Implementations;
using mitocode.netfullstack.services.Interfaces;

namespace mitocode.netfullstack.services
{
    public static class InjectionDependency
    {
        public static IServiceCollection AddInjection(this IServiceCollection services)
        {
            return services.AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<IProductService, ProductService>()
                .AddTransient<ICategoryRepository, CategoryRepository>()
                .AddTransient<ICategoryService, CategoryService>();
        }
    }
}