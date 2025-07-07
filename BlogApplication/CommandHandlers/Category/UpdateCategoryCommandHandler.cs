using BlogApplication.Abstractions;
using BlogApplication.Commands.Category;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers.Category;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, BlogDomain.Models.Category>
{
    private readonly ICategoryService _categoryService;
    public UpdateCategoryCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<BlogDomain.Models.Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new BlogDomain.Models.Category
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description
        };
        return await _categoryService.UpdateCategory(category);
    }
} 