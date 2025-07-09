using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Post>
{
    private readonly IBlogService _blogService;
    private readonly ICategoryService _categoryService;
    private readonly ITagService _tagService;
    public UpdatePostCommandHandler(IBlogService blogService, ICategoryService categoryService, ITagService tagService)
    {
        _blogService = blogService;
        _categoryService = categoryService;
        _tagService = tagService;
    }

    public async Task<Post> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var tagNames = request.Tags.Select(n => n.ToLower().Trim())
            .Where(nm => !string.IsNullOrEmpty(nm)).Distinct().ToList();
        var existedTags = await _tagService.GetTagsByNames(tagNames);
        var newTagsNames = tagNames.Where(tag => !existedTags.Select(t => t.Name).Contains(tag));
        List<Tag> tagsToAdd = new List<Tag>(existedTags);
        
        if (newTagsNames.Any())
        {
            tagsToAdd.AddRange(await _tagService.CreateTags(newTagsNames.Select(tag => new Tag { Name = tag }).ToList()));
        }
        
        var blog = new Post
        {
            Id = request.Id,
            Title = request.Title,
            Content = request.Content,
            Category = await _categoryService.GetCategoryById(request.CategoryId),
            Privacy = (Privacy)Enum.Parse(typeof(Privacy), request.Privacy, true),
            PathsToImages = request.PathsToImages,
            IsBlocked = request.IsBlocked,
            Tags = tagsToAdd
        };
        return await _blogService.UpdateBlog(blog);
    }
} 