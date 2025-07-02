using Microsoft.EntityFrameworkCore;
using UserInfrastucture.Data;

namespace UserAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<UserDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("DataBaseMigrator")));
        builder.Services.AddControllers();
        var app = builder.Build();
        app.MapControllers();
        
        
        app.Run();
    }
}