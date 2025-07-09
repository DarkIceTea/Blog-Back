using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private readonly ISender _sender;
    public BlogController(ISender sender)
    {
        _sender = sender;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllBlogs()
    {
        return Ok(await _sender.Send(new GetAllPostsCommand()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlogById([FromRoute]Guid id)
    {
        return Ok(await _sender.Send(new GetPostByIdCommand() {Id = id}));
    }
    [HttpPost]
    public async Task<IActionResult> CreateBlog([FromBody] CreatePostCommand post, CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(post));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBlog([FromRoute]Guid id, [FromBody] UpdatePostCommand post, CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(post));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlog([FromRoute]Guid id)
    {
        await _sender.Send(new DeletePostCommand() { Id = id });
        return Ok();
    }
    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        // This endpoint can be used to check the health of the API
        return Ok("API is running smoothly!");
    }
}