using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Starting database migration...");
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(connectionString, b => b.MigrationsAssembly("DataBaseMigrator")));
    })
    .Build();
    
Console.WriteLine("Host configured.");

ApplyMigrations(host.Services);

static async Task ApplyMigrations(IServiceProvider services)
{
    using var scope = services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

    try
    {
        Console.WriteLine("Applying migrations...");
        
        await dbContext.Database.MigrateAsync();
        
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

    