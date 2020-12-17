using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherWitch.Models
{
  public class Temperature
  {

    [JsonProperty("day")]
    public double AverageTemperature { get; set; }
    [JsonProperty("min")]
    public double MinimumTemperature { get; set; }
    [JsonProperty("max")]
    public double MaximumTemperature { get; set; }
  }
}
