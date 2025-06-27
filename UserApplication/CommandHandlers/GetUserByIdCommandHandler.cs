using MediatR;
using UserApplication.Commands;
using UserDomain;

namespace UserApplication.CommandHandlers;

public class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand, Profile>
{
    public Task<Profile> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}