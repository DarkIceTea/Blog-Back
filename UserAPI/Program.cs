using Application.Commands.RegisterUser;
using Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using UserApplication.Abstractions;
using UserApplication.Commands;
using UserInfrastucture.Data;
using UserInfrastucture.Services;

namespace UserAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<UserDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("DataBaseMigrator")));
        builder.Services.AddValidatorsFromAssembly(typeof(CreateUserCommand).Assembly);
        builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<CreateUserCommand>());
        builder.Services.AddScoped<IProfileService, ProfileService>();
        builder.Services.AddControllers();
        var app = builder.Build();
        app.MapControllers();
        
        
        app.Run();
    }
}