using MediatR;
using UserApplication.Commands;
using UserDomain;

namespace UserApplication.CommandHandlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Profile>
{
    public Task<Profile> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}