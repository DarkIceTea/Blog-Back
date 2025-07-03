using MediatR;
using UserApplication.Abstractions;
using UserApplication.Commands;
using UserDomain;

namespace UserApplication.CommandHandlers;

public class GetUsersCommandHandler(IProfileService _service) : IRequestHandler<GetUsersCommand, List<Profile>>
{
    public async Task<List<Profile>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
    {
        return await _service.GetUsersAsync();
    }
}