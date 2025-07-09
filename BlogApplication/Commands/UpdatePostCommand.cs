using BlogDomain.Models;

namespace BlogApplication.Commands;
using MediatR;

public class UpdatePostCommand : IRequest<Post>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public string Privacy { get; set; }
    public string[] PathsToImages { get; set; } = Array.Empty<string>();
    public bool IsBlocked { get; set; }
    public string[] Tags { get; set; } = Array.Empty<string>();
}