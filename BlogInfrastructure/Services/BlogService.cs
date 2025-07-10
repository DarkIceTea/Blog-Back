using BlogApplication.Abstractions;
using BlogDomain.Models;
using BlogInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogInfrastructure.Services;

public class BlogService(BlogDbContext _context) : IBlogService
{
    public async Task<Post> CreateBlog(Post post)
    {
        if (post == null)
        {
            throw new ArgumentNullException(nameof(post), "Blog cannot be null");
        }

        // Assuming _context.Blogs is a DbSet<Blog>
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();

        return post;
    }

    public async Task<Post> GetBlogById(Guid id)
    {
        var blog = await _context.Posts.Include(b => b.Category)
            .Include(b => b.Tags).FirstOrDefaultAsync(b => b.Id == id);

        if (blog == null)
        {
            throw new KeyNotFoundException($"Blog with ID {id} not found");
        }

        return blog;
    }

    public async Task<List<Post>> GetAllBlogs()
    {
        // Assuming _context.Blogs is a DbSet<Blog>
        return await _context.Posts.Include(b => b.Category)
            .Include(b => b.Tags).ToListAsync();
    }

    public async Task<Post> UpdateBlog(Post post)
    {
        if (post == null)
        {
            throw new ArgumentNullException(nameof(post), "Blog cannot be null");
        }

        _context.Posts.Update(post);
        await _context.SaveChangesAsync();

        return post;
    }

    public async Task DeleteBlog(Guid id)
    {
        // Assuming _context.Blogs is a DbSet<Blog>
        var blog = await _context.Posts.FindAsync(id);

        if (blog == null)
        {
            throw new KeyNotFoundException($"Blog with ID {id} not found");
        }

        _context.Posts.Remove(blog);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Post>> GetBlogsByCategoryId(Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Post>> GetBlogsByCategoryName(string categoryName)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Post>> GetBlogsByAuthorId(Guid authorId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Post>> GetBlogsByAuthorName(string authorName)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Post>> GetBlogsByTitle(string title)
    {
        throw new NotImplementedException();
    }
}