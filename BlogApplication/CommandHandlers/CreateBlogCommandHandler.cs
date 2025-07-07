using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, Blog>
{
    private readonly IBlogService _blogService;
    private readonly ICategoryService _categoryService;
    public CreateBlogCommandHandler(IBlogService blogService, ICategoryService categoryService)
    {
        _blogService = blogService;
        _categoryService = categoryService;
    }

    public async Task<Blog> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var blog = new Blog
        {
            Title = request.Title,
            Content = request.Content,
            Category = await _categoryService.GetCategoryById(request.CategoryId),
            Privacy = (Privacy)Enum.Parse(typeof(Privacy), request.Privacy, true),
            AuthorId = request.AuthorId,
            PathsToImages = request.PathsToImages
        };
        return await _blogService.CreateBlog(blog);
    }
} 