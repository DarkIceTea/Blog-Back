using MediatR;
using UserApplication.Abstractions;
using UserApplication.Commands;
using UserDomain;

namespace UserApplication.CommandHandlers;

public class GetUserByIdCommandHandler(IProfileService _service) : IRequestHandler<GetUserByIdCommand, Profile>
{
    public async Task<Profile> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
    {
        return await _service.GetUserByIdAsync(request.Id);
    }
}