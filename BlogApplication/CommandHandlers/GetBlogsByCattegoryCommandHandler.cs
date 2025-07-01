using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class GetBlogsByCattegoryCommandHandler : IRequestHandler<GetBlogsByCattegoryCommand, List<Blog>>
{
    private readonly IBlogService _blogService;
    public GetBlogsByCattegoryCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<List<Blog>> Handle(GetBlogsByCattegoryCommand request, CancellationToken cancellationToken)
    {
        return await _blogService.GetBlogsByCategoryId(request.CategoryId);
    }
} 