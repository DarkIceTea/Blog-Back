using BlogApplication.Abstractions;
using BlogDomain.Models;
using BlogInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogInfrastructure.Services;

public class BlogService(BlogDbContext _context) : IBlogService
{
    public async Task<Blog> CreateBlog(Blog blog)
    {
        if (blog == null)
        {
            throw new ArgumentNullException(nameof(blog), "Blog cannot be null");
        }

        // Assuming _context.Blogs is a DbSet<Blog>
        await _context.Blogs.AddAsync(blog);
        await _context.SaveChangesAsync();
        
        return blog;
    }

    public async Task<Blog> GetBlogById(Guid id)
    {
        // Assuming _context.Blogs is a DbSet<Blog>
        var blog = await _context.Blogs.FindAsync(id);
        
        if (blog == null)
        {
            throw new KeyNotFoundException($"Blog with ID {id} not found");
        }

        return blog;
    }

    public async Task<List<Blog>> GetAllBlogs()
    {
        // Assuming _context.Blogs is a DbSet<Blog>
        return await _context.Blogs.Include(b => b.Category).ToListAsync();
    }

    public async Task<Blog> UpdateBlog(Blog blog)
    {
        if (blog == null)
        {
            throw new ArgumentNullException(nameof(blog), "Blog cannot be null");
        }

        _context.Blogs.Update(blog);
        await _context.SaveChangesAsync();
        
        return blog;
    }

    public async Task DeleteBlog(Guid id)
    {
        // Assuming _context.Blogs is a DbSet<Blog>
        var blog = await _context.Blogs.FindAsync(id);
        
        if (blog == null)
        {
            throw new KeyNotFoundException($"Blog with ID {id} not found");
        }

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Blog>> GetBlogsByCategoryId(Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Blog>> GetBlogsByCategoryName(string categoryName)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Blog>> GetBlogsByAuthorId(Guid authorId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Blog>> GetBlogsByAuthorName(string authorName)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Blog>> GetBlogsByTitle(string title)
    {
        throw new NotImplementedException();
    }
}