using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogDomain.Models;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class SearchBlogsByTitleCommandHandler(IBlogService _blogService) : IRequestHandler<SearchBlogsByTitleCommand, List<Post>>
{
    public async Task<List<Post>> Handle(SearchBlogsByTitleCommand request, CancellationToken cancellationToken)
    {
        return await _blogService.SearchBlogByTitle(request.Title);
    }
}