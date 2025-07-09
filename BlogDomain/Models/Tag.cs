namespace BlogDomain.Models;

public record Tag()
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; }
    public List<Post> Blogs { get; init; }
}