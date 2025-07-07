using BlogApplication.Abstractions;
using BlogApplication.Commands.Category;
using MediatR;

namespace BlogApplication.CommandHandlers.Category;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly ICategoryService _categoryService;
    public DeleteCategoryCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await _categoryService.DeleteCategory(request.Id);
        return Unit.Value;
    }
} 