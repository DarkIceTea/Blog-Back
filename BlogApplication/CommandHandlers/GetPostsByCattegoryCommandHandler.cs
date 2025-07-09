using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class GetPostsByCattegoryCommandHandler : IRequestHandler<GetPostsByCattegoryCommand, List<Post>>
{
    private readonly IBlogService _blogService;
    public GetPostsByCattegoryCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<List<Post>> Handle(GetPostsByCattegoryCommand request, CancellationToken cancellationToken)
    {
        return await _blogService.GetBlogsByCategoryId(request.CategoryId);
    }
} 