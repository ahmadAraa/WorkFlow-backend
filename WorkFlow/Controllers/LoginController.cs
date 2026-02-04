using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.ViewModels;
using Services;
using System.Threading.Tasks;

namespace WorkFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        protected readonly UserService _userService;
        protected readonly JwtService _jwtService;
        protected readonly ILogger<LoginController> _logger;

        public LoginController(UserService userService, JwtService jwtService, ILogger<LoginController> logger)
        {
            _jwtService = jwtService;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("user-signup")]
        public async Task<IActionResult> signup([FromBody] UserVM userVM)
        { 
            _logger.LogInformation("User signup initiated for email: {Email}", userVM.Email);
            _userService.AddUser(userVM);
            // Retrieve the user via login with the provided credentials
            var userLogin = new UserLoginVM 
            { 
                Email = userVM.Email, 
                Password = userVM.Password 
            };
            
            var user =await _userService.Login(userLogin);
            if(user == null)
            {
                _logger.LogWarning("Signup failed during login step for email: {Email}", userVM.Email);
                return Unauthorized("Signup failed.");
            }
            
            string token =_jwtService.CreateToken(user);
            _logger.LogInformation("User signup successful for email: {Email}", userVM.Email);
            return Ok(new { AccessToken = token });
        }

        [HttpPost("user-login")]
        public async Task<IActionResult> login([FromBody] UserLoginVM userVM)
        {
            _logger.LogInformation("User login attempt for email: {Email}", userVM.Email);
            var user =await _userService.Login(userVM);
            if (user == null)
            {
                _logger.LogWarning("Login failed for email: {Email}", userVM.Email);
                return Unauthorized("Invalid username or password.");
            }
            string token =  _jwtService.CreateToken(user);
            _logger.LogInformation("User login successful for email: {Email}", userVM.Email);
            return Ok(new { AccessToken = token });
        }
    }
}
