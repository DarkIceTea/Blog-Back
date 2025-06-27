using MediatR;
using UserApplication.Commands;
using UserDomain;

namespace UserApplication.CommandHandlers;

public class GetUsersCommandHandler : IRequestHandler<GetUsersCommand, List<Profile>>
{
    public Task<List<Profile>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}