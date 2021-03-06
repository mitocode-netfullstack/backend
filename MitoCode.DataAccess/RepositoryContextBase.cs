using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mitocode.netfullstack.entities;

namespace mitocode.netfullstack.dataaccess
{
    public class RepositoryContextBase<TEntityBase> where TEntityBase : EntityBase, new()
    {
        protected MitoCodeDbContext Context;

        protected RepositoryContextBase(MitoCodeDbContext context)
        {
            Context = context;
        }

        public virtual async Task<ICollection<TInfo>> ListCollection<TInfo>(Expression<Func<TEntityBase, TInfo>> selector) where TInfo : class, new()
        {
            return await Context.Set<TEntityBase>()
                .OrderBy(p => p.Id)
                .AsNoTracking()
                .Select(selector)
                .ToListAsync();
        }

        protected virtual async Task<(ICollection<TEntityBase> collection, int total)> ListCollection(Expression<Func<TEntityBase, bool>> predicate, int page, int rows)
        {
            var collection = await Context.Set<TEntityBase>()
                .Where(predicate).OrderBy(p => p.Id)
                .AsNoTracking()
                .Skip((page - 1) * rows)
                .Take(rows)
                .ToListAsync();

            var totalCount = await Context.Set<TEntityBase>()
                .Where(predicate)
                .AsNoTracking()
                .CountAsync();

            return (collection.ToList(), totalCount);
        }

        public virtual async Task<(ICollection<TInfo> collection, int total)> ListCollectionAsync<TInfo>(
            Expression<Func<TEntityBase, TInfo>> selector,
            Expression<Func<TEntityBase, bool>> predicate,
            int page,
            int rows)
            where TInfo : class, new()
        {
            var collection = await Context.Set<TEntityBase>()
                .Where(predicate).OrderBy(p => p.Id)
                .AsNoTracking()
                .Select(selector)
                .Skip((page - 1) * rows)
                .Take(rows)
                .ToListAsync();

            var totalCount = await Context.Set<TEntityBase>()
                .Where(predicate)
                .AsNoTracking()
                .CountAsync();

            return (collection.ToList(), totalCount);
        }

        protected virtual async Task<TEntityBase> Select(int id)
        {
            var entity = await Context.Set<TEntityBase>()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (entity == null)
                throw new InvalidOperationException("El registro no existe!");

            return entity;
        }
    }
}