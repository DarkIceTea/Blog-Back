namespace BlogApplication.Commands.Category;
using BlogDomain.Models;
using MediatR;

public class GetCategoryByIdCommand : IRequest<Category>
{
    public Guid Id { get; set; }
}