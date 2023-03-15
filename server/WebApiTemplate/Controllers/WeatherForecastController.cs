using Microsoft.AspNetCore.Mvc;
using EmeraldChameleonChat.DAL.Repository.RepositoryInterfaces;
using EmeraldChameleonChat.Model.DTO;
using EmeraldChameleonChat.DAL.Repository;
using EmeraldChameleonChat.AutoMapperProfiles;
using EmeraldChameleonChat.Model.Entity;

namespace EmeraldChameleonChat.Controllers
{
    [ApiController]
    [Route("/api/WeatherForecast")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastRepository _weatherForecastRepository;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger
            ,IWeatherForecastRepository weatherForecastRepository)
        {
            _logger = logger;
            _weatherForecastRepository = weatherForecastRepository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult<IEnumerable<WeatherForecastGetDto>>> Get(CancellationToken token)
        {
            _logger.LogDebug("Example Debug Log Message");
            var weatherForecastInfoFromDatabaseViaRepository = (await _weatherForecastRepository.GetAsync(token));
            var result = weatherForecastInfoFromDatabaseViaRepository.MapToDTO<IEnumerable<WeatherForecastGetDto>>();
            return Ok(result);
        }
    }
}