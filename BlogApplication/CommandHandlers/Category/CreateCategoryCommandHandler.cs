using BlogApplication.Abstractions;
using BlogApplication.Commands.Category;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers.Category;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BlogDomain.Models.Category>
{
    private readonly ICategoryService _categoryService;
    public CreateCategoryCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<BlogDomain.Models.Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new BlogDomain.Models.Category
        {
            Name = request.Name,
            Description = request.Description
        };
        return await _categoryService.CreateCategory(category);
    }
} 