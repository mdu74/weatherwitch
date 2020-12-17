using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherWitch.Models;

namespace WeatherWitch.Services
{
  public interface IWeatherService
  {
    WeatherInformation GetCurrentWeather(Coordinates coordinates);
    IEnumerable<HourlyWeatherInfo> GetHourlyWeatherForecast(Coordinates coordinates);
    IEnumerable<DailyWeatherInfo> GetDailyWeatherForecast(Coordinates coordinates);
  }
}
