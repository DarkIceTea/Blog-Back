using BlogApplication.Commands;
using BlogDomain.Models;
using BlogInfrastructure.Dto;
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
    public async Task<IActionResult> GetBlogs([FromQuery]string? searchTerm, CancellationToken cancellationToken)
    {
        List<Post> posts = new List<Post>();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            posts = await _sender.Send(new SearchBlogsByTitleCommand() { Title = searchTerm });
        }
        else
        {
            posts = await _sender.Send(new GetAllPostsCommand());
        }
        return Ok(
        
            posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty,
                AuthorId = p.AuthorId,
                IsBlocked = p.IsBlocked,
                Privacy = p.Privacy.ToString(),
                Tags = p.Tags.Select(t => new TagDto
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList(),
                PathsToImages = p.PathsToImages
            }).ToList()
        );
    }

    [HttpGet("tags")]
    public async Task<IActionResult> GetPostsbyTag([FromQuery] string? tag)
    {
        if (string.IsNullOrEmpty(tag))
        {
            return BadRequest("Tag cannot be null or empty.");
        }
        
        var posts = await _sender.Send(new GetPostsByTagCommand() { TagName = tag });
        
        return Ok(posts.Select(p => new PostDto
        {
            Id = p.Id,
            Title = p.Title,
            Content = p.Content,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,
            CategoryId = p.CategoryId,
            CategoryName = p.Category?.Name ?? string.Empty,
            AuthorId = p.AuthorId,
            IsBlocked = p.IsBlocked,
            Privacy = p.Privacy.ToString(),
            Tags = p.Tags.Select(t => new TagDto
            {
                Id = t.Id,
                Name = t.Name
            }).ToList(),
            PathsToImages = p.PathsToImages
        }).ToList());
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
    public async Task<IActionResult> GetBlogsByTag([FromQuery] string tagName)
    {
        throw new NotImplementedException();
        //return Ok(await _sender.Send(new GetPostsByTagCommand() { TagName = tagName }));
    }
    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        // This endpoint can be used to check the health of the API
        return Ok("API is running smoothly!");
    }
}