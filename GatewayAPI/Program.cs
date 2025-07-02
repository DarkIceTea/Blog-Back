using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Net.Http;
namespace GatewayAPI;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddHttpClient(); 

        builder.Services.AddControllers();
        
        builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
        builder.Services.AddOcelot(builder.Configuration);
        
        var app = builder.Build();
        
        app.UseHttpsRedirection();
        
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
        
        await app.UseOcelot(); 

        app.Run();
    }
}