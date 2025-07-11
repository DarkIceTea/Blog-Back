using BlogDomain.Models;
using MediatR;

namespace BlogApplication.Commands;

public class SearchBlogsByTitleCommand : IRequest<List<Post>>
{
    public string Title { get; set; }
    
}