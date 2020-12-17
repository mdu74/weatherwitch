using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WeatherWitch.Dto;
using WeatherWitch.Helpers;
using WeatherWitch.Models;

namespace WeatherWitch.Services
{
  public class UserService : IUserService
  {
    private List<User> _users = new List<User>
    {
        new User { Email = "mdu@yahoo.com", Password = "Password123" },
        new User { Email = "otherguy@yahoo.com", Password = "Password456" }
    };

    private readonly AppSettings _appSettings;

    public UserService(IOptions<AppSettings> appSettings)
    {
      _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
    }

    public AuthResponse Authenticate(AuthRequest userInfo)
    {
      var user = _users.SingleOrDefault(x => x.Email == userInfo.Email && x.Password == userInfo.Password);

      if (user == null) return null;

      var token = GenerateJwtToken(user);

      return new AuthResponse(user, token);
    }

    public User GetByEmail(string Email)
    {
      return _users.FirstOrDefault(x => x.Email == Email);
    }

    private string GenerateJwtToken(User user)
    {      
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[] { new Claim("Email", user.Email.ToString()) }),
        Expires = DateTime.UtcNow.AddDays(7), // generate token that is valid for 7 days
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}
