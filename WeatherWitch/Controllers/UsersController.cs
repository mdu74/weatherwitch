using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherWitch.Models;
using WeatherWitch.Services;

namespace WeatherWitch.Controllers
{
  [ApiController]
  public class UsersController : ControllerBase
  {
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
      _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpPost]
    [Route("api/User/LogIn")]
    public IActionResult Login(AuthRequest userLogin) 
    {
      var response = _userService.Authenticate(userLogin);
      if (response == null) return BadRequest(new { message = "Your login information is incorrect"});

      return Ok(response);
    }
  }
}
