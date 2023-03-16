using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;
using EmeraldChameleonChat.DAL.DbContexts;
using EmeraldChameleonChat.DAL.Repository;
using EmeraldChameleonChat.DAL.Repository.RepositoryInterfaces;
using EmeraldChameleonChat.Model.Entity;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("/logs/output.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddAutoMapper(x =>
{
    x.IncludeSourceExtensionMethods(typeof(IEntity));
},AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EmeraldChameleonChatContext>(
    dbContextOptions => dbContextOptions.UseSqlite("Data Source=WeatherInfo.db"));// adds the dbcontext with a scoped lifetime
builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();