using BlogApplication.Abstractions;
using BlogApplication.Commands;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, Unit>
{
    private readonly IBlogService _blogService;
    public DeleteBlogCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<Unit> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
    {
        await _blogService.DeleteBlog(request.Id);
        return Unit.Value;
    }
} 