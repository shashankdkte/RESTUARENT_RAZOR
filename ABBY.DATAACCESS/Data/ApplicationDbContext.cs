using ABBY.MODELS;
using Microsoft.EntityFrameworkCore;

namespace ABBY.DATAACCESS
{
    public class ApplicationDbContext:DbContext
 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<FoodType> FoodType { get; set; }
    }
}
