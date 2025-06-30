using FluentValidation;
using MediatR;
using UserApplication.Abstractions;
using UserApplication.Commands;
using UserDomain;

namespace UserApplication.CommandHandlers;

public class CreateUserCommandHandler(IUserService _service, IValidator<Profile> validator) : IRequestHandler<CreateUserCommand, Profile>
{
    public async Task<Profile> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var profile = new Profile()
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth,
            PathToAvatar = request.PathToAvatar
        };
        validator.ValidateAndThrow(profile);
        return await _service.CreateUserAsync(profile);
    }
}