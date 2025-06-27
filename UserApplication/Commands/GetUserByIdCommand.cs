using MediatR;
using UserDomain;

namespace UserApplication.Commands;

public class GetUserByIdCommand : IRequest<Profile>
{
    public Guid Id { get; set; }
}