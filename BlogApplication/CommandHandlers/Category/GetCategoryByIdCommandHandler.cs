using BlogApplication.Abstractions;
using BlogApplication.Commands.Category;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers.Category;

public class GetCategoryByIdCommandHandler : IRequestHandler<GetCategoryByIdCommand, BlogDomain.Models.Category>
{
    private readonly ICategoryService _categoryService;
    public GetCategoryByIdCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<BlogDomain.Models.Category> Handle(GetCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        return await _categoryService.GetCategoryById(request.Id);
    }
} 