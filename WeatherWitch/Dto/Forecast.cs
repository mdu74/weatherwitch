using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherWitch.Models
{
  public class Forecast
  {
    [JsonProperty("lon")]
    public double Lon { get; set; }
    [JsonProperty("lat")]
    public double Lat { get; set; }
    [JsonProperty("timezone")]

    public string Timezone { get; set; }
    [JsonProperty("timezone_offset")]
    public string TimezoneOffset { get; set; }
    [JsonProperty("daily")]
    public ICollection<DailyWeather> Daily { get; set; }
  }
}
