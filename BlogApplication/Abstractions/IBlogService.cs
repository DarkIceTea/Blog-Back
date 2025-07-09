using BlogDomain.Models;

namespace BlogApplication.Abstractions;

public interface IBlogService
{
    public Task<Post> CreateBlog(Post post);
    public Task<Post> GetBlogById(Guid id);
    public Task<List<Post>> GetAllBlogs();
    public Task<Post> UpdateBlog(Post post);
    public Task DeleteBlog(Guid id);
    public Task<List<Post>> GetBlogsByCategoryId(Guid categoryId);
    public Task<List<Post>> GetBlogsByCategoryName(string categoryName);
    public Task<List<Post>> GetBlogsByAuthorId(Guid authorId);
    public Task<List<Post>> GetBlogsByAuthorName(string authorName);
    public Task<List<Post>> GetBlogsByTitle(string title);
}