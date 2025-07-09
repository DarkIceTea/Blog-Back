using BlogDomain.Models;
using MediatR;

namespace BlogApplication.Commands;

public class GetPostsByCattegoryCommand : IRequest<List<Post>>
{
    public Guid CategoryId { get; set; }
}