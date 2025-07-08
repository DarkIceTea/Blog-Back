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
        return Ok(await _sender.Send(new GetAllBlogsCommand()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlogById([FromRoute]Guid id)
    {
        return Ok(await _sender.Send(new GetBlogByIdCommand() {Id = id}));
    }
    [HttpPost]
    public async Task<IActionResult> CreateBlog([FromBody] CreateBlogCommand blog, CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(blog));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBlog([FromRoute]Guid id, [FromBody] UpdateBlogCommand blog, CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(blog));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlog([FromRoute]Guid id)
    {
        await _sender.Send(new DeleteBlogCommand() { Id = id });
        return Ok();
    }
    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        // This endpoint can be used to check the health of the API
        return Ok("API is running smoothly!");
    }
}