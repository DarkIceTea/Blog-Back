using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, Blog>
{
    private readonly IBlogService _blogService;
    public CreateBlogCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<Blog> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var blog = new Blog
        {
            Title = request.Title,
            Content = request.Content,
            Category = new Category { Id = request.CategoryId },
            Privacy = request.Privacy,
            AuthorId = request.AuthorId,
            PathsToImages = request.PathsToImages
        };
        return await _blogService.CreateBlog(blog);
    }
} 