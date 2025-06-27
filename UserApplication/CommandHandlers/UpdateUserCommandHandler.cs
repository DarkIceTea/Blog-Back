using MediatR;
using UserApplication.Commands;
using UserDomain;

namespace UserApplication.CommandHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Profile>
{
    public Task<Profile> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}