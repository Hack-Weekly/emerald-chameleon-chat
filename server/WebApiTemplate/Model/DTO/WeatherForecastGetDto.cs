using System.ComponentModel.DataAnnotations;

namespace EmeraldChameleonChat.Model.DTO
{
    public class WeatherForecastGetDto : IDTO
    {
        public Guid Id { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; } = String.Empty;
        public double TemperatureF { get { return TemperatureC * 1.8 + 32; } } //this is a Calculated Property on the DTO - not stored in the database. (See  Model.Entity.WeatherForecast.cs)
        public string CityName { get; set; } = String.Empty;
    }
}
