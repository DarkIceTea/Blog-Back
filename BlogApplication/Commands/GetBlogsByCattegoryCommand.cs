using BlogDomain.Models;
using MediatR;

namespace BlogApplication.Commands;

public class GetBlogsByCattegoryCommand : IRequest<List<Blog>>
{
    public Guid CategoryId { get; set; }
}