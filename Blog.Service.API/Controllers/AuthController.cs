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

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await _authService.RegisterUser(model);
            
            if (result)
            {
                return Ok(new { Message = "User registered successfully" });
            }
            return BadRequest(new { Message = "User registration failed" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var result = await _authService.LoginUser(loginModel);
            var tokenString = _authService.GennerateTokenString(loginModel);
            if (result)
            {
                return Ok(new { Message = "Login successful", tokenString });
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
