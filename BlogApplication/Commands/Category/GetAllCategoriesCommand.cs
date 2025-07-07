namespace BlogApplication.Commands.Category;
using BlogDomain.Models;
using MediatR;
using System.Collections.Generic;

public class GetAllCategoriesCommand : IRequest<List<Category>>
{
}