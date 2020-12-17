using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherWitch.Models
{
  public class WeatherInformation
  {
    public string City { get; set; }
    public string Country { get; set; }
    public string Time { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string Description { get; set; }
    public string Humidity { get; set; }
    public string TemperatureFeelsLike { get; set; }
    public string Temperature { get; set; }
    public string MaximumTemperature { get; set; }
    public string MinimunTemperature { get; set; }
    public string WeatherIcon { get; set; }
  }
}
