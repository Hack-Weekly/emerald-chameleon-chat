using Microsoft.EntityFrameworkCore;
using EmeraldChameleonChat.Services.DAL.DbContexts;
using EmeraldChameleonChat.Services.Model.Entity;
using EmeraldChameleonChat.Services.DAL.Repository.RepositoryInterfaces;

namespace EmeraldChameleonChat.Services.DAL.Repository
{
    public class WeatherForecastRepository : BaseRepository<WeatherForecast>, IWeatherForecastRepository //this class EXTENDS our base repository & implements the repositories interface
    {
        private readonly EmeraldChameleonChatContext _context;

        //our constructor/class extends the base class from above so all of those implementations are implemented here 
        //therefore we meet the 'contract' requirements of the interface
        public WeatherForecastRepository(EmeraldChameleonChatContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
