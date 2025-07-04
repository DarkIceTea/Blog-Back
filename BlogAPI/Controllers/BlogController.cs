using BlogApplication.Commands;
using BlogDomain.Models;
using BlogInfrastructure.DTO;
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
    [HttpGet("get")]
    public async Task<IActionResult> GetAllBlogs()
    {
        return Ok(await _sender.Send(new GetAllBlogsCommand()));
    }
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetBlogById([FromQuery]Guid id)
    {
        return Ok(await _sender.Send(new GetBlogByIdCommand() {Id = id}));
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateBlog([FromBody] CreateBlogCommand blog, CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(blog));
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateBlog([FromQuery]Guid id, [FromBody] Blog blog, CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(new UpdateBlogCommand()
        {
            Id = id,
            Title = blog.Title,
            Content = blog.Content,
            CategoryId = blog.Category.Id,
            Privacy = blog.Privacy,
            PathsToImages = blog.PathsToImages,
            IsBlocked = blog.IsBlocked
        }));
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteBlog(Guid id)
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