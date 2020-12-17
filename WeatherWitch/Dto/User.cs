using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherWitch.Dto
{
  public class User
  {
    public string Email { get; set; }

    [JsonIgnore]
    public string Password { get; set; }
  }
}
