namespace BlogApplication.Commands;
using MediatR;

public class DeletePostCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}