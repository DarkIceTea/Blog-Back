using BlogApplication.Abstractions;
using BlogDomain.Models;
using BlogInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogInfrastructure.Services;

public class CategoryService(BlogDbContext _context) : ICategoryService
{
    public async Task<Category> CreateCategory(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category), "Category cannot be null");
        }

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> GetCategoryById(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Invalid category ID", nameof(id));
        }

        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with ID {id} not found");
        }

        return category;
    }

    public async Task<List<Category>> GetAllCategory()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> UpdateCategory(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category), "Category cannot be null");
        }

        var existingCategory = await _context.Categories.FindAsync(category.Id);
        if (existingCategory == null)
        {
            throw new KeyNotFoundException($"Category with ID {category.Id} not found");
        }
        _context.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task DeleteCategory(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Invalid category ID", nameof(id));
        }

        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with ID {id} not found");
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}