using BlogDomain.Models;

namespace BlogApplication.Commands;
using System;
using System.Collections.Generic;
using MediatR;

public class GetPostsByAuthorIdCommand : IRequest<List<Post>>
{
    public Guid AuthorId { get; set; }
}