using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherWitch.Models;
using WeatherWitch.Services;

namespace WeatherWitch.Controllers
{
  [ApiController]
  public class WeatherForecastController : ControllerBase
  {
    private IWeatherService _weatherService;
    public WeatherForecastController(IWeatherService weatherService)
    {
      _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
    }

    [HttpPut]
    [Route("api/WeatherForecast/GetWeatherForecast")]
    public async Task<IActionResult> GetWeatherForecast(Coordinates coordinates)
    {
      var response = _weatherService.GetCurrentWeather(coordinates);

      if (response == null) return BadRequest(new { message = "Couldn't get the current weather" });

      return Ok(response);
    }

    [HttpPut]
    [Route("api/WeatherForecast/GetWeatherForecastHourly")]
    public async Task<IActionResult> GetWeatherForecastHourly(Coordinates coordinates)
    {
      var response = _weatherService.GetHourlyWeatherForecast(coordinates);
      
      if (response == null) return BadRequest(new { message = "Couldn't get the hourly weather forecast" });

      return Ok(response);
    }


    [HttpPut]
    [Route("api/WeatherForecast/GetWeatherForecastDaily")]
    public async Task<IActionResult> GetWeatherForecastDaily(Coordinates coordinates)
    {
      var response = _weatherService.GetDailyWeatherForecast(coordinates);

      if (response == null) return BadRequest(new { message = "Couldn't get the daily weather forecast" });

      return Ok(response);
    }

  }
}
