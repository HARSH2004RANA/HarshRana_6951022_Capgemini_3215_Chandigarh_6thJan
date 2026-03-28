using Microsoft.EntityFrameworkCore;
using ProductApiCache.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductApiCache.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        
        public DbSet<User> Users { get; set; }
    }
}