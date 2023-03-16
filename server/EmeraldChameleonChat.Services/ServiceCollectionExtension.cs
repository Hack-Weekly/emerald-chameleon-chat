using EmeraldChameleonChat.Services.DAL.DbContexts;
using EmeraldChameleonChat.Services.DAL.Repository.RepositoryInterfaces;
using EmeraldChameleonChat.Services.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

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
            //services.AddDbContext<EmeraldChameleonChatContext>(dbContextOptions => dbContextOptions.UseSqlite("Data Source=WeatherInfo.db"));// adds the dbcontext with a scoped lifetime
            
            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();


            return services;
        }
    }
}
