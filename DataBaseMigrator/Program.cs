using BlogInfrastructure.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserInfrastucture.Data;

Console.WriteLine("Starting database migration...");
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine($"Using connection string: {connectionString}");
        services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(connectionString, b => b.MigrationsAssembly("DataBaseMigrator")));
        services.AddDbContext<BlogDbContext>(options =>
            options.UseNpgsql(connectionString, b => b.MigrationsAssembly("DataBaseMigrator")));
        services.AddDbContext<UserDbContext>(options =>
            options.UseNpgsql(connectionString, b => b.MigrationsAssembly("DataBaseMigrator")));
    })
    .Build();
    
Console.WriteLine("Host configured.");

await ApplyMigrationsAsync(host.Services);

static async Task ApplyMigrationsAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();

    var authDbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    var blogDbContext = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
    var userDbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();

    try
    {
        Console.WriteLine("Applying migrations...");
        
        await authDbContext.Database.MigrateAsync();
        await blogDbContext.Database.MigrateAsync();
        await userDbContext.Database.MigrateAsync();
        
        Console.WriteLine("Migrations applied successfully.");
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Migration error: {ex.Message}");
        Console.ResetColor();
        throw; 
    }
}

    