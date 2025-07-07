using MediatR;
namespace BlogApplication.Commands.Category;

public class DeleteCategoryCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}