using BlogDomain.Models;
using MediatR;

namespace BlogApplication.Commands.Category;

public class UpdateCategoryCommand : IRequest<BlogDomain.Models.Category>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}