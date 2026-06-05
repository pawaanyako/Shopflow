using Catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Data
{
    public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
    }
}
