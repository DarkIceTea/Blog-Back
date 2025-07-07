using BlogApplication.Abstractions;
using BlogApplication.Commands.Category;
using BlogDomain.Models;
using MediatR;
using System.Collections.Generic;

namespace BlogApplication.CommandHandlers.Category;

public class GetAllCategoriesCommandHandler : IRequestHandler<GetAllCategoriesCommand, List<BlogDomain.Models.Category>>
{
    private readonly ICategoryService _categoryService;
    public GetAllCategoriesCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<List<BlogDomain.Models.Category>> Handle(GetAllCategoriesCommand request, CancellationToken cancellationToken)
    {
        return await _categoryService.GetAllCategory();
    }
} 