using BlogApplication.Abstractions;
using BlogApplication.Commands;
using MediatR;

namespace BlogApplication.CommandHandlers;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Unit>
{
    private readonly IBlogService _blogService;
    public DeletePostCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        await _blogService.DeleteBlog(request.Id);
        return Unit.Value;
    }
} 