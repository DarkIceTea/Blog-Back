using BlogDomain.Models;

namespace BlogApplication.Abstractions;

public interface IBlogService
{
    public Task<Blog> CreateBlog(Blog blog);
    public Task<Blog> GetBlogById(Guid id);
    public Task<List<Blog>> GetAllBlogs();
    public Task<Blog> UpdateBlog(Blog blog);
    public Task DeleteBlog(Guid id);
    public Task<List<Blog>> GetBlogsByCategoryId(Guid categoryId);
    public Task<List<Blog>> GetBlogsByCategoryName(string categoryName);
    public Task<List<Blog>> GetBlogsByAuthorId(Guid authorId);
    public Task<List<Blog>> GetBlogsByAuthorName(string authorName);
    public Task<List<Blog>> GetBlogsByTitle(string title);
}