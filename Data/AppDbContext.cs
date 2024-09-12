using Microsoft.EntityFrameworkCore;
using learncode.Models;

namespace learncode.Data;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
        public DbSet<User> Users { get; set; } = null!;

        
    }

