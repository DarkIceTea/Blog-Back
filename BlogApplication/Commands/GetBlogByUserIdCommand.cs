using BlogDomain.Models;

namespace BlogApplication.Commands;
using System;
using System.Collections.Generic;
using MediatR;

public class GetBlogByUserIdCommand : IRequest<List<Blog>>
{
    public Guid AuthorId { get; set; }
}