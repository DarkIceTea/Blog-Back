using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, Blog>
{
    private readonly IBlogService _blogService;
    private readonly ICategoryService _categoryService;
    public UpdateBlogCommandHandler(IBlogService blogService, ICategoryService categoryService)
    {
        _blogService = blogService;
        _categoryService = categoryService;
    }

    public async Task<Blog> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        var blog = new Blog
        {
            Id = request.Id,
            Title = request.Title,
            Content = request.Content,
            Category = await _categoryService.GetCategoryById(request.CategoryId),
            Privacy = (Privacy)Enum.Parse(typeof(Privacy), request.Privacy, true),
            PathsToImages = request.PathsToImages,
            IsBlocked = request.IsBlocked
        };
        return await _blogService.UpdateBlog(blog);
    }
} 