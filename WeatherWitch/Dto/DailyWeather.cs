using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherWitch.Models
{
  public class DailyWeather
  {
    [JsonProperty("dt")]
    public int Day { get; set; }
    [JsonProperty("weather")]
    public List<Weather> Weather { get; set; }
    [JsonProperty("temp")]
    public List<Temperature> Temperature { get; set; }

  }
}
