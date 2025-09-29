using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using Services;

namespace WorkFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        protected readonly UserService _userService;
        protected readonly JwtService _jwtService;

public LoginController(UserService userService, JwtService jwtService)
        {
            this._jwtService = jwtService;
            this._userService = userService;    
        }

        [HttpPost("user-signup")]
        public IActionResult signup([FromBody] UserVM userVM) { 
            _userService.AddUser(userVM);
            // Retrieve the user via login with the provided credentials
            var userLogin = new UserLoginVM 
            { 
                Email = userVM.Email, 
                Password = userVM.Password 
            };
            var user = _userService.Login(userLogin);
            if(user == null)
                return Unauthorized("Signup failed.");
            string token = _jwtService.CreateToken(user);
            return Ok(new { AccessToken = token });
        }
        [HttpPost("user-login")]
        public IActionResult login([FromBody] UserLoginVM userVM) {
            var user = _userService.Login(userVM);
            if (user == null) return Unauthorized("Invalid username or password.");
            string token = _jwtService.CreateToken(user);
            return Ok(new { AccessToken = token });
        }
        

    }
}
