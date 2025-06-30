using MediatR;

namespace UserApplication.Commands;

public class DeleteUserCommand : IRequest
{
    public Guid Id { get; set; }
}