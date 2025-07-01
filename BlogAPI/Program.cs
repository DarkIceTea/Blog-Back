using BlogApplication.Abstractions;
using BlogApplication.Commands;
using BlogInfrastructure.Data;
using BlogInfrastructure.Services;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateBlogCommand>());
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("DataBaseMigrator")));
var app = builder.Build();
app.MapControllers();
        
app.Run();