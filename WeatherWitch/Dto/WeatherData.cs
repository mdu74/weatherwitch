using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherWitch.Models
{
  public class WeatherData
  {
    public double Temp { get; set; }
    public double Feels_like { get; set; }
    public double Temp_min { get; set; }
    public double Temp_max { get; set; }
    public double Pressure { get; set; }
    public double Humidity { get; set; }
  }
}
