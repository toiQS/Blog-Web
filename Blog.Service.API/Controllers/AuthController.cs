using Blog.Service._auth;
using Blog_Model;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser(RegisterModel model)
        {
            var result = await _authService.RegisterUser(model);
            
            if (result)
            {
                return Ok(new { Message = "User registered successfully" });
            }
            return BadRequest(new { Message = "User registration failed" });
        }
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterModel model)
        {
            var result = await _authService.RegisterAdmin(model);
            
            if (result)
            {
                return Ok(new { Message = "Admin registered successfully" });
            }
            return BadRequest(new { Message = "User registration failed" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var result = await _authService.LoginUser(loginModel);
            var tokenString = await _authService.GennerateTokenString(loginModel);
            var detail = await _authService.GetUserDetail(loginModel.Email);
            
            if (result)
            {
                return Ok(new { Message = "Login successful", tokenString, detail });
            }
            return Unauthorized(new { Message = "Invalid email or password" });
        }
        [HttpPost("logout")]
        public async Task<IActionResult> LogOutUser()
        {
            await _authService.LogOutUser();
            return Ok(new { Message = "Logout successful" });
        }
    }
}
