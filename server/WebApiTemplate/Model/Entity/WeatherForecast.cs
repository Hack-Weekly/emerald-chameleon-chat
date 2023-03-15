using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmeraldChameleonChat.Model.Entity
{
    public class WeatherForecast : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "City Name field is required.")]
        [MaxLength(50)]
        public string CityName { get; set; }

        [Required(ErrorMessage = "TemperatureC field is required.")]

        public double TemperatureC { get; set; }

        [MaxLength(250)]
        public string Summary { get; set; }
        
        //if we were creatomg a ICollection we would instantiate it to a new List<T> 

        public WeatherForecast(string cityName, double temperatureC) //adding a constructor ensures the default parameterless constructor is no-longer generated. 
            //This is conveying that we want our WeatherForecast class to always have a CityName and TemperatureC value.
        {
            CityName = cityName;
            TemperatureC = temperatureC;
        }
    }
}

