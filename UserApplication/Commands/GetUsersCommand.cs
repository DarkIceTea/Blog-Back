using MediatR;
using UserDomain;

namespace UserApplication.Commands;

public class GetUsersCommand : IRequest<List<Profile>>
{
    
}