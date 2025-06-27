using MediatR;
using UserDomain;

namespace UserApplication.Commands;

public class CreateUserCommand : IRequest<Profile>
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public string PhoneNumber { get; init; } = string.Empty;
    public string PathToAvatar { get; init; } = string.Empty;
}