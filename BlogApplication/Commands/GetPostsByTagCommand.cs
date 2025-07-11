using BlogDomain.Models;
using MediatR;

namespace BlogApplication.Commands;

public class GetPostsByTagCommand : IRequest<List<Post>>
{
    public string TagName {get; set;}
}