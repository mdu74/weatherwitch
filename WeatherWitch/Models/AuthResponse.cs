using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherWitch.Dto;

namespace WeatherWitch.Models
{
  public class AuthResponse
  {
    public string Email { get; set; }
    public string Token { get; set; }


    public AuthResponse(User user, string token)
    {
      Email = user.Email;
      Token = token;
    }
  }
}
