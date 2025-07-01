using BlogDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogInfrastructure.Data;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configure your entity mappings here
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    // Define DbSet properties for your entities
    // public DbSet<Blog> Blogs { get; set; }
    // public DbSet<Post> Posts { get; set; }
    // Add other DbSet properties as needed
}