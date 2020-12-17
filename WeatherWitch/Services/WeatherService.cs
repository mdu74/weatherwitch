using Nancy.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WeatherWitch.Models;

namespace WeatherWitch.Services
{
  public class WeatherService : IWeatherService
  {
    private readonly string apiKey = "5d1e95a006a3250e406038f5a20d51df";

    public WeatherInformation GetCurrentWeather(Coordinates coordinates)
    {
      var url = $"http://api.openweathermap.org/data/2.5/weather?q=Pinetown&units=metric&cnt=1&APPID={apiKey}";
      // TODO: Make this dynamic, only using hard coded location for testing for the brief
      //var lat = coordinates.Lat;
      //var lon = coordinates.Lon;
      //var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&exclude=daily&cnt=1&appid={apiKey}";
      
      var json = GetRequest(url);
      var weatherStats = (new JavaScriptSerializer()).Deserialize<WeatherStats>(json);

      var weatherInformation = new WeatherInformation
      {
        Country = weatherStats.Sys.Country,
        City = weatherStats.Name,
        Latitude = Convert.ToString(weatherStats.Coord.Lat),
        Longitude = Convert.ToString(weatherStats.Coord.Lon),
        Description = weatherStats.Weather[0].Description,
        Humidity = Convert.ToString(weatherStats.Main.Humidity),
        Temperature = Convert.ToString(weatherStats.Main.Temp),
        TemperatureFeelsLike = Convert.ToString(weatherStats.Main.Feels_like),
        MaximumTemperature = Convert.ToString(weatherStats.Main.Temp_max),
        MinimunTemperature = Convert.ToString(weatherStats.Main.Temp_min),
        WeatherIcon = weatherStats.Weather[0].Icon
      };

      return weatherInformation;
    }

    public IEnumerable<HourlyWeatherInfo> GetHourlyWeatherForecast(Coordinates coordinates) 
    {
      var lat = coordinates.Lat;
      var lon = coordinates.Lon;
      var url = $"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&units=metric&exclude=daily&appid={apiKey}";
      // var url = $"https://api.openweathermap.org/data/2.5/onecall?lat=-29.806988&lon=30.853558&units=metric&exclude=daily&cnt={days}&appid={apiKey}"; hard coded location
      var json = GetRequest(url);
      var weatherStats = JObject.Parse(json);

      var hourlyWeatherJson = (JArray)weatherStats["hourly"];
      var futureHourlyForcast = new List<HourlyWeatherInfo>();

      var hourlyWeather = hourlyWeatherJson.Select(i => {
        var items = new HourlyWeatherInfo
        {
          Hour = ConvertTimestampToDate(i["dt"].ToString(), "hourly"),
          HourTemperature = double.Parse(i["temp"].ToString())
        };
        return items;
      });

      return hourlyWeather;
    }

    public IEnumerable<DailyWeatherInfo> GetDailyWeatherForecast(Coordinates coordinates)
    {
      var lat = coordinates.Lat;
      var lon = coordinates.Lon;
      var days = 7;
      var url = $"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&units=metric&exclude=hourly&cnt={days}&appid={apiKey}";
      var json = GetRequest(url);
      var weatherStats = JObject.Parse(json);

      var hourlyWeatherJson = (JArray)weatherStats["daily"];
      var futureHourlyForcast = new List<DailyWeatherInfo>();

      var hourlyWeather = hourlyWeatherJson.Select(data => {
        var items = new DailyWeatherInfo
        {
          Day = ConvertTimestampToDate(data["dt"].ToString(), "daily"),
          DaysTemperature =  double.Parse(data["temp"]["day"].ToString())
        };

        return items;
      });

      return hourlyWeather;
    }

    private static string ConvertTimestampToDate(string timestamp, string breakDown)
    {
      var getDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
      getDate = getDate.AddSeconds(int.Parse(timestamp));
      if(breakDown == "daily") return getDate.DayOfWeek.ToString();
      else return $"{getDate:HH:mm}";
    }
    //Just in case the WebClient bombs out
    internal class WebClient : System.Net.WebClient
    {
      public int Timeout { get; set; }

      protected override WebRequest GetWebRequest(Uri uri)
      {
        WebRequest lWebRequest = base.GetWebRequest(uri);
        lWebRequest.Timeout = Timeout;
        ((HttpWebRequest)lWebRequest).ReadWriteTimeout = Timeout;
        return lWebRequest;
      }
    }

    private string GetRequest(string url)
    {
      using (var lWebClient = new WebClient())
      {
        lWebClient.Timeout = 600 * 60 * 1000;
        return lWebClient.DownloadString(url);
      }
    }
  }
}
