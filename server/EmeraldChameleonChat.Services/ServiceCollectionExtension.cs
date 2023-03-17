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
            services.AddDbContext<EmeraldChameleonChatContext>(dbContextOptions => dbContextOptions.UseSqlite(Configuration["ConnectionStrings:SQLLite"]));// adds the dbcontext with a scoped lifetime

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
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


            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
