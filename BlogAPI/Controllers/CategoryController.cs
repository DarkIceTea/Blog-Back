using BlogApplication.Commands;
using BlogApplication.Commands.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ISender _sender;
    public CategoryController(ISender sender)
    {
        _sender = sender;
    }
    [HttpGet("get")]
    public async Task<IActionResult> GetAllCategories()
    {
        return Ok(await _sender.Send(new GetAllCategoriesCommand()));
    }
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetBlogById([FromQuery]Guid id)
    {
        return Ok(await _sender.Send(new GetCategoryByIdCommand() {Id = id}));
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateBlog([FromBody] CreateCategoryCommand blog, CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(blog));
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateBlog([FromQuery]Guid id, [FromBody] UpdateCategoryCommand blog, CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(blog));
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteBlog(Guid id)
    {
        await _sender.Send(new DeleteCategoryCommand() { Id = id });
        return Ok();
    }
    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        // This endpoint can be used to check the health of the API
        return Ok("API is running smoothly!");
    }
}