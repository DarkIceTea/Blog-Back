using MediatR;
using UserApplication.Abstractions;
using UserApplication.Commands;

namespace UserApplication.CommandHandlers;

public class DeleteUserCommandHandler(IUserService _service) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteUserAsync(request.Id);
    }
}