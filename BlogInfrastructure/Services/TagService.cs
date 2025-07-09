using BlogApplication.Abstractions;
using BlogDomain.Models;
using BlogInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogInfrastructure.Services;

public class TagService(BlogDbContext _context) : ITagService
{
    public async Task<Tag> CreateTag(Tag tag)
    {
        if (tag == null)
        {
            throw new ArgumentNullException(nameof(tag), "Tag cannot be null");
        }

        // Assuming _context.Tags is a DbSet<Tag>
        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();
        
        return tag;
    }

    public async Task<Tag> GetTagById(Guid id)
    {
        // Assuming _context.Tags is a DbSet<Tag>
        var tag = await _context.Tags.FindAsync(id);
        
        if (tag == null)
        {
            throw new KeyNotFoundException($"Tag with ID {id} not found");
        }

        return tag;
    }

    public async Task<List<Tag>> GetAllTags()
    {
        return await _context.Tags.ToListAsync();
    }

    public async Task DeleteTag(Guid id)
    {
        var tag = await _context.Tags.FindAsync(id);
        
        if (tag == null)
        {
            throw new KeyNotFoundException($"Tag with ID {id} not found");
        }

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Tag>> GetTagsByNames(List<string> names)
    {
        return await _context.Tags
            .Where(tag => names.Contains(tag.Name))
            .ToListAsync();
    }

    public async Task<List<Tag>> CreateTags(List<Tag> tags)
    {
        if (tags == null || !tags.Any())
        {
            throw new ArgumentNullException(nameof(tags), "Tags cannot be null or empty");
        }
        await _context.Tags.AddRangeAsync(tags);
        await _context.SaveChangesAsync();
        
        return tags;
    }
}