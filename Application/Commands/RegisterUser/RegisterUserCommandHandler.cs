using Application.Abstractions;
using Application.Results;
using MediatR;

namespace Application.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(IUserService service) : IRequestHandler<RegisterUserCommand, (Tokens, Guid)>
    {
        public async Task<(Tokens, Guid)> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await service.RegisterUserAsync(request.Email, request.UserName, request.Password, request.UserRole, cancellationToken);
        }
    }
}
