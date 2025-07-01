using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, Blog>
{
    private readonly IBlogService _blogService;
    public UpdateBlogCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<Blog> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        var blog = new Blog
        {
            Id = request.Id,
            Title = request.Title,
            Content = request.Content,
            Category = new Category { Id = request.CategoryId },
            Privacy = request.Privacy,
            PathsToImages = request.PathsToImages,
            IsBlocked = request.IsBlocked
        };
        return await _blogService.UpdateBlog(blog);
    }
} 