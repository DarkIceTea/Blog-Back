using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class GetAllPostsCommandHandler : IRequestHandler<GetAllPostsCommand, List<Post>>
{
    private readonly IBlogService _blogService;
    public GetAllPostsCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<List<Post>> Handle(GetAllPostsCommand request, CancellationToken cancellationToken)
    {
        return await _blogService.GetAllBlogs();
    }
} 