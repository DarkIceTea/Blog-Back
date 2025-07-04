using BlogDomain.Models;
using MediatR;

namespace BlogApplication.Commands;

public class CreateBlogCommand : IRequest<Blog>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid[] CategoryIds { get; set; }
    public string Privacy { get; set; } = "Public";
    public Guid AuthorId { get; set; }
    public string[] PathsToImages { get; set; } = Array.Empty<string>();
}