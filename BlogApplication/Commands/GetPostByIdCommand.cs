using BlogDomain.Models;

namespace BlogApplication.Commands;
using MediatR;

public class GetPostByIdCommand : IRequest<Post>
{
    public Guid Id { get; set; }
}