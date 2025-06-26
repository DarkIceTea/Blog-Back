﻿using Application.Options;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace API.Configuration
{
    public static class AuthConfiguration
    {
        public static WebApplicationBuilder Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<CustomUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<AccessTokenOptions>(builder.Configuration.GetSection("AccessTokenOptions"));

            //builder.Services.AddAuthorization();
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidIssuer = "AuthApiServer",
            //            ValidateAudience = true,
            //            ValidAudience = "InnoclinicApi",
            //            ValidateLifetime = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("securitykeysecuritykeysecuritykey")),
            //            ValidateIssuerSigningKey = true,
            //        };
            //    });
            return builder;
        }
    }
}
