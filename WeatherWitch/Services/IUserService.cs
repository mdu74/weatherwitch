using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherWitch.Dto;
using WeatherWitch.Models;

namespace WeatherWitch.Services
{
  public interface IUserService
  {
    AuthResponse Authenticate(AuthRequest user);
    User GetByEmail(string email);
  }
}
