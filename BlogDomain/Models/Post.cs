namespace BlogDomain.Models;

public record Post()
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;
    public Guid CategoryId { get; init; }
    public Category Category { get; init; }
    public Privacy Privacy { get; init; } 
    public Guid AuthorId { get; init; } 
    public bool IsBlocked { get; init; } = false;
    public List<Tag> Tags { get; init; } 
    public string[] PathsToImages { get; init; } = Array.Empty<string>();
};