using Microsoft.EntityFrameworkCore;

namespace webapiCRUD.Models
{
    public class BrandContext:DbContext
    {
        public BrandContext(DbContextOptions<BrandContext> options):base(options)
        {
            
        }
        public DbSet<Brand> brands { get; set; }
    }
}
