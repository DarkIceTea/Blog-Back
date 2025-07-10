using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserApplication.Commands;
using UserDomain;

namespace UserAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    public UserController(ISender sender)
    {
        _sender = sender;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _sender.Send(new GetUsersCommand()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromRoute]Guid id)
    {
        return Ok(await _sender.Send(new GetUserByIdCommand() {Id = id}));
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] Profile profile, CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(new CreateUserCommand
        {
            Id = profile.Id,
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            Email = profile.Email,
            DateOfBirth = profile.DateOfBirth,
            PhoneNumber = profile.PhoneNumber,
            PathToAvatar = profile.PathToAvatar
        }, cancellationToken));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute]Guid id, [FromBody] Profile profile)
    {
        return Ok(await _sender.Send(new UpdateUserCommand
        {
            Id = id, 
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            Email = profile.Email,
            DateOfBirth = profile.DateOfBirth,
            PhoneNumber = profile.PhoneNumber,
            PathToAvatar = profile.PathToAvatar
        }));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute]Guid id)
    {
        await _sender.Send(new DeleteUserCommand { Id = id });
        return Ok();
    }
    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        // This endpoint can be used to check the health of the API
        return Ok("API is running smoothly!");
    }
}