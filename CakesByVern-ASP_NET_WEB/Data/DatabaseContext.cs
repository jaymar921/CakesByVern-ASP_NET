using Microsoft.EntityFrameworkCore;

namespace CakesByVern_ASP_NET_WEB.Data
{
    public class DatabaseContext: DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<CakesByVern_Data.Entity.User> Users { get; set; }
        public DbSet<CakesByVern_Data.Entity.Post> Posts { get; set; }
        public DbSet<CakesByVern_Data.Entity.Product> Products { get; set; }
        public DbSet<CakesByVern_Data.Entity.Order> Orders { get; set; }
    }
}
