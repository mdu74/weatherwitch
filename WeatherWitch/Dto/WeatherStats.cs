using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherWitch.Models
{
  public class WeatherStats
  {
    public int id { get; set; }
    public string Name { get; set; }
    public Coordinates Coord { get; set; }
    public List<Weather> Weather { get; set; }
    public string @Base { get; set; }
    public WeatherData Main { get; set; }
    public int Visibility { get; set; }
    public WindData Wind { get; set; }
    public Clouds Clouds { get; set; }
    public int Dt { get; set; }
    public System Sys { get; set; }
    public int Timezone { get; set; }
    public int Cod { get; set; }
    [JsonProperty("dt_txt")]
    public string Time { get; set; }
  }
}
