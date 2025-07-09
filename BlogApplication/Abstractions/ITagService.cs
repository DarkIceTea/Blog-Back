using BlogDomain.Models;

namespace BlogApplication.Abstractions;

public interface ITagService
{
    public Task<Tag> CreateTag(Tag tag);
    public Task<Tag> GetTagById(Guid id);
    public Task<List<Tag>> GetAllTags();
    public Task DeleteTag(Guid id);
    public Task<List<Tag>> GetTagsByNames(List<string> names);
    public Task<List<Tag>> CreateTags(List<Tag> tags);
}