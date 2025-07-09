using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class GetPostByIdCommandHandler : IRequestHandler<GetPostByIdCommand, Post>
{
    private readonly IBlogService _blogService;
    public GetPostByIdCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<Post> Handle(GetPostByIdCommand request, CancellationToken cancellationToken)
    {
        return await _blogService.GetBlogById(request.Id);
    }
} 