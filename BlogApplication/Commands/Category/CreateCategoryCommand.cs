namespace BlogApplication.Commands.Category;
using BlogDomain.Models;
using MediatR;

public class CreateCategoryCommand : IRequest<Category>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}