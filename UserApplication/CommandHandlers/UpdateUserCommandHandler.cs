using FluentValidation;
using MediatR;
using UserApplication.Abstractions;
using UserApplication.Commands;
using UserDomain;

namespace UserApplication.CommandHandlers;

public class UpdateUserCommandHandler(IProfileService _service, IValidator<Profile> _validator) : IRequestHandler<UpdateUserCommand, Profile>
{
    public async Task<Profile> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
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
        //_validator.ValidateAndThrow(profile);
        return await _service.UpdateUserAsync(profile);
    }
}