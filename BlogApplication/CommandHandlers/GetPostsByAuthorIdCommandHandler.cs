using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class GetPostsByAuthorIdCommandHandler : IRequestHandler<GetPostsByAuthorIdCommand, List<Post>>
{
    private readonly IBlogService _blogService;
    public GetPostsByAuthorIdCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<List<Post>> Handle(GetPostsByAuthorIdCommand request, CancellationToken cancellationToken)
    {
        return await _blogService.GetBlogsByAuthorId(request.AuthorId);
    }
} 