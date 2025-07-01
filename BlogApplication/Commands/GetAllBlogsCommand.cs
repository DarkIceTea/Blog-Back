using BlogDomain.Models;

namespace BlogApplication.Commands;
using MediatR;

public class GetAllBlogsCommand : IRequest<List<Blog>>
{
    
}