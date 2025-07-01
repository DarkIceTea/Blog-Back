using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class GetBlogByUserIdCommandHandler : IRequestHandler<GetBlogByUserIdCommand, List<Blog>>
{
    private readonly IBlogService _blogService;
    public GetBlogByUserIdCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<List<Blog>> Handle(GetBlogByUserIdCommand request, CancellationToken cancellationToken)
    {
        return await _blogService.GetBlogsByAuthorId(request.AuthorId);
    }
} 