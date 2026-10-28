using Microsoft.EntityFrameworkCore;

namespace eCommerce.api.Models
{
    public class ECommerceDbContext: DbContext
    {
        // create a constructor, then we provide this into the program.cs
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {

        }

        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }

        public DbSet<ProductImageModel> ProductImageModels { get; set; }
    }
}
