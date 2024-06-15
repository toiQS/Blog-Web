using Blog.Service._auth;
using Blog_Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Service.API.Controllers
{
    /// <summary>
    /// Controller to manage authentication-related operations via API.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the AuthController class.
        /// </summary>
        /// <param name="authService">The service to handle authentication-related operations.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The registration model containing user data.</param>
        /// <returns>The result of the user registration operation.</returns>
        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterUser(model);

            if (result)
            {
                return Ok(new { Message = "User registered successfully" });
            }
            return BadRequest(new { Message = "User registration failed", result });
        }

        /// <summary>
        /// Registers a new admin.
        /// </summary>
        /// <param name="model">The registration model containing admin data.</param>
        /// <returns>The result of the admin registration operation.</returns>
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAdmin(model);

            if (result)
            {
                return Ok(new { Message = "Admin registered successfully" });
            }
            return BadRequest(new { Message = "Admin registration failed", result });
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginModel">The login model containing user credentials.</param>
        /// <returns>The result of the login operation.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginUser(loginModel);
            if (result)
            {
                var tokenString = await _authService.GennerateTokenString(loginModel);
                

                return Ok(new { Message = "Login successful", Token = tokenString});
            }
            return Unauthorized(new { Message = "Invalid email or password", result });
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <returns>The result of the logout operation.</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> LogOutUser()
        {
            await _authService.LogOutUser();
            return Ok(new { Message = "Logout successful" });
        }
    }
}
