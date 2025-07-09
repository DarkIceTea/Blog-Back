using BlogDomain.Models;
using MediatR;

namespace BlogApplication.Commands;

public class CreatePostCommand : IRequest<Post>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public string Privacy { get; set; } = "Public";
    public Guid AuthorId { get; set; }
    public string[] PathsToImages { get; set; } = Array.Empty<string>();
    public string[] Tags { get; set; } = Array.Empty<string>();
}