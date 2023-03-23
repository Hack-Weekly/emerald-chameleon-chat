using EmeraldChameleonChat.Services.DAL.DbContexts;
using EmeraldChameleonChat.Services.DAL.Repository.RepositoryInterfaces;
using EmeraldChameleonChat.Services.DAL.Repository;
using EmeraldChameleonChat.Services.Repository;
using EmeraldChameleonChat.Services.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace EmeraldChameleonChat.Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddProjectServicesCollections(this IServiceCollection services, IConfiguration Configuration)
        {
            var connectionString = Configuration.GetConnectionString("HackWeekly");
            
            services.AddDbContext<EmeraldChameleonChatContext>(options => options.UseMySql(
                (connectionString), ServerVersion.AutoDetect(connectionString)
                ));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/chatHub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
                //options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["Jwt:Keys:Access"]))
                };
            });

            services.AddScoped<IChatRoomMessageRepository, ChatRoomMessageRepository>();
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEmailService, EmailService>();


            return services;
        }
    }
}
