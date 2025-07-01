using BlogDomain.Models;

namespace BlogApplication.Commands;
using MediatR;

public class GetBlogByIdCommand : IRequest<Blog>
{
    public Guid Id { get; set; }
}