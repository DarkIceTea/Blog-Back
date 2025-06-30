using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private readonly ISender _sender;
    UserController(ISender sender)
    {
        _sender = sender;
    }
    [HttpGet("get")]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _sender.Send());
    }
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetUserById([FromQuery]Guid id)
    {
        return Ok(await _sender.Send());
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody], IValidator<> validator, CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send());
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, )
    {
        return Ok(await _sender.Send());
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _sender.Send();
        return Ok();
    }
    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        // This endpoint can be used to check the health of the API
        return Ok("API is running smoothly!");
    }
}