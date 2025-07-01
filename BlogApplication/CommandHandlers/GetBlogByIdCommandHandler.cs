using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class GetBlogByIdCommandHandler : IRequestHandler<GetBlogByIdCommand, Blog>
{
    private readonly IBlogService _blogService;
    public GetBlogByIdCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<Blog> Handle(GetBlogByIdCommand request, CancellationToken cancellationToken)
    {
        return await _blogService.GetBlogById(request.Id);
    }
} 