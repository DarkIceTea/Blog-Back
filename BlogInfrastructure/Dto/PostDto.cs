namespace BlogInfrastructure.Dto;

public record PostDto()
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;
    public Guid CategoryId { get; init; }
    public string CategoryName { get; init; } = string.Empty;
    public string Privacy { get; init; } 
    public Guid AuthorId { get; init; } 
    public bool IsBlocked { get; init; } = false;
    public List<TagDto> Tags { get; init; } 
    public string[] PathsToImages { get; init; } = Array.Empty<string>();
}