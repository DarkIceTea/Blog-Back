namespace BlogApplication.Commands;
using MediatR;

public class DeleteBlogCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}