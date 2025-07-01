using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class GetAllBlogsCommandHandler : IRequestHandler<GetAllBlogsCommand, List<Blog>>
{
    private readonly IBlogService _blogService;
    public GetAllBlogsCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<List<Blog>> Handle(GetAllBlogsCommand request, CancellationToken cancellationToken)
    {
        return await _blogService.GetAllBlogs();
    }
} 