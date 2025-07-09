using BlogDomain.Models;

namespace BlogApplication.Commands;
using MediatR;

public class GetAllPostsCommand : IRequest<List<Post>>
{
    
}