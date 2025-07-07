using BlogDomain.Models;

namespace BlogApplication.Abstractions;

public interface ICategoryService
{
    public Task<Category> CreateCategory(Category category);
    public Task<Category> GetCategoryById(Guid id);
    public Task<List<Category>> GetAllCategory();
    public Task<Category> UpdateCategory(Category category);
    public Task DeleteCategory(Guid id);
}