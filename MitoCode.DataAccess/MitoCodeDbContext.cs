using Microsoft.EntityFrameworkCore;
using mitocode.netfullstack.entities;

namespace mitocode.netfullstack.dataaccess
{
    public class MitoCodeDbContext : DbContext
    {
        public MitoCodeDbContext(DbContextOptions<MitoCodeDbContext> options) : base(options)
        {
        }

        public MitoCodeDbContext()
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}