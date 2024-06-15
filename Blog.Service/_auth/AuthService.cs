using Blog_Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Service._auth
{
    /// <summary>
    /// Service for handling authentication-related operations.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly string _filePath = @"F:\Repo\Local\Blog-Web\Blog.Service\_auth\Memory.txt";

        /// <summary>
        /// Initializes a new instance of the AuthService class.
        /// </summary>
        /// <param name="userManager">Manages user-related operations.</param>
        /// <param name="signInManager">Handles sign-in operations.</param>
        /// <param name="configuration">Provides configuration settings.</param>
        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerModel">The model containing user registration details.</param>
        /// <returns>True if registration succeeds; otherwise, false.</returns>
        public async Task<bool> RegisterUser(RegisterModel registerModel)
        {
            var user = new IdentityUser
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception($"Error registering user: {ex.Message}");
            }
        }

        /// <summary>
        /// Registers a new admin.
        /// </summary>
        /// <param name="registerModel">The model containing admin registration details.</param>
        /// <returns>True if registration succeeds; otherwise, false.</returns>
        public async Task<bool> RegisterAdmin(RegisterModel registerModel)
        {
            var user = new IdentityUser
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception($"Error registering admin: {ex.Message}");
            }
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginModel">The model containing user login details.</param>
        /// <returns>True if login succeeds; otherwise, false.</returns>
        public async Task<bool> LoginUser(LoginModel loginModel)
        {
            var identityUser = await _userManager.FindByEmailAsync(loginModel.Email);
            if (identityUser == null)
            {
                return false;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(identityUser, loginModel.Password, false);
            return result.Succeeded;
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public async Task LogOutUser()
        {
            await _signInManager.SignOutAsync();
            ClearTemporaryMemory();
        }

        /// <summary>
        /// Generates a JWT token string for the authenticated user.
        /// </summary>
        /// <param name="loginModel">The model containing user login details.</param>
        /// <returns>The JWT token string.</returns>
        public async Task<string> GennerateTokenString(LoginModel loginModel)
        {
            var identityUser = await _userManager.FindByEmailAsync(loginModel.Email);
            if (identityUser == null)
            {
                return "false";
            }

            var role = await _userManager.GetRolesAsync(identityUser);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, loginModel.Email),
                new Claim(ClaimTypes.Role, role[0])
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        /// <summary>
        /// Retrieves user details by email.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <returns>The result containing user details.</returns>
        public async Task<ServiceResult<UserDetail>> GetUserDetail(string email)
        {
            try
            {
                var identityUser = await _userManager.FindByEmailAsync(email);
                if (identityUser == null)
                {
                    return ServiceResult<UserDetail>.FailedResult("User not found.");
                }

                var user = new UserDetail
                {
                    Email = identityUser.Email,
                    UserID = identityUser.Id,
                    UserName = identityUser.UserName
                };

                await CreateTemporaryMemoryAsync(user.Email);
                return ServiceResult<UserDetail>.SuccessResult(user);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                return ServiceResult<UserDetail>.FailedResult($"Error retrieving user details: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a temporary memory file with the user's email.
        /// </summary>
        /// <param name="email">The user's email.</param>
        private async Task CreateTemporaryMemoryAsync(string email)
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }

                await File.AppendAllTextAsync(_filePath, email);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error creating temporary memory: {ex.Message}");
            }
        }

        /// <summary>
        /// Clears the temporary memory file.
        /// </summary>
        private void ClearTemporaryMemory()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error clearing temporary memory: {ex.Message}");
            }
        }
    }
}
