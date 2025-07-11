using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class GetPostsByTagCommandHandler(IBlogService _blogService) : IRequestHandler<GetPostsByTagCommand, List<Post>>
{
    public async Task<List<Post>> Handle(GetPostsByTagCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.TagName))
        {
            throw new ArgumentException("Tag name cannot be null or empty.", nameof(request.TagName));
        }

        // Assuming _blogService is an instance of IBlogService injected into this handler
        var posts = await _blogService.GetBlogsByTagName(request.TagName);

        return posts;
    }
}