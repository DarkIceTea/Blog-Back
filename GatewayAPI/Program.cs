using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GatewayAPI;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddHttpClient(); 

        builder.Services.AddControllers();
        
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
                };
            });
        builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
        builder.Services.AddOcelot(builder.Configuration);
        builder.Services.AddSwaggerForOcelot(builder.Configuration);
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("gateway", new OpenApiInfo { Title = "API Gateway", Version = "v1" });
        });
        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
        
        var app = builder.Build();
        app.UseCors("AllowAllOrigins");
        app.UseSwagger();
        
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway");
            c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
            
            var ocelotConfig = app.Services.GetRequiredService<IConfiguration>();
            var swaggerEndPoints = ocelotConfig.GetSection("SwaggerEndPoints").GetChildren();

            foreach (var endPoint in swaggerEndPoints)
            {
                var key = endPoint.GetValue<string>("Key");
                var name = endPoint.GetValue<string>("Config:0:Name");
                var url = $"/swagger/docs/{key}"; 
                c.SwaggerEndpoint(url, name);
            }
        });
        
        app.UseHttpsRedirection();
        
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        
        app.MapWhen(context => context.GetEndpoint() == null, ocelotApp =>
        {
            ocelotApp.UseOcelot().Wait();
        });

        app.Run();
    }
}