namespace BlogDomain.Models;

public record Category()
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    List<Blog> Blogs { get; init; }
};