using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mitocode.netfullstack.entities;
using mitocode.netfullstack.entities.Complex;

namespace mitocode.netfullstack.dataaccess.Repositories
{
    public class ProductRepository : RepositoryContextBase<Product>, IProductRepository
    {
        public ProductRepository(MitoCodeDbContext context) : base(context)
        {
        }

        public async Task<(ICollection<ProductInformation> collection, int total)> GetCollectionAsync(string filter, int page, int rows)
        {
            var tupla = await List(p => p.ProductName.Contains(filter), page, rows);
            var collection = tupla.collection.ToList();
            return (collection, tupla.total);
        }

        private async Task<(ICollection<ProductInformation> collection, int total)> List(Expression<Func<Product, bool>> predicate, int page, int rows)
        {
            var collection = await Context.Set<Product>()
                .Where(predicate).OrderBy(p => p.Id)
                .Select(p => new ProductInformation
                {
                    Id = p.Id,
                    Name = p.ProductName,
                    CategoryName = p.Category.Name,
                    UnitPrice = p.UnitPrice,
                })
                .AsNoTracking()
                .Skip((page - 1) * rows)
                .Take(rows)
                .ToListAsync();

            var totalCount = await Context.Set<Product>()
                .Where(predicate)
                .AsNoTracking()
                .CountAsync();

            return (collection.ToList(), totalCount);
        }

        public async Task<Product> GetItemAsync(int id)
        {
            return await Select(id);
        }

        public async Task<int> CreateAsync(Product entity)
        {
            return await Context.Insert(entity);
        }

        public async Task UpdateAsync(Product entity)
        {
            await Context.UpdateEntity(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await Context.Delete(new Product
            {
                Id = id
            });
        }
    }
}