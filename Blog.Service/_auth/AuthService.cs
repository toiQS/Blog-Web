using Blog_Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Service._auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        string filePath = @"F:\Repo\Local\Blog-Web\Blog.Service\_auth\Memory.txt";

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

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
                var resultAddToRoles = await _userManager.AddToRoleAsync(user, "User");
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.ToString());
            }
        }
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
                var resultAddToRoles = await _userManager.AddToRoleAsync(user, "Admin");
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.ToString());
            }
        }
        
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
        public async Task LogOutUser()
        {
            ClearTemporaryMemory();
            await _signInManager.SignOutAsync();
        }
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
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:SecretKey").Value));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            SecurityToken security = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(16),
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(security);
            return tokenString;
        }
        public async Task<ServiceResult<UserDetai>> GetUserDetail(string email)
        {
            try
            {
                var identityUser = await _userManager.FindByEmailAsync(email);
                if (identityUser == null)
                {
                    throw new Exception();
                }
                var user = new UserDetai
                {
                    Email = identityUser.Email,
                    UserID = identityUser.Id,
                    UserName = identityUser.UserName
                };
                await CreateTemporaryMemoryAsync(user.Email);
                return ServiceResult<UserDetai>.SuccessResult(user);
            }
            catch (Exception ex)
            {
                return ServiceResult<UserDetai>.FailedResult(ex.Message);
            }

        }
        public async Task CreateTemporaryMemoryAsync( string email)
        {
            try
            {
                
                File.Delete(filePath);
                await File.AppendAllTextAsync(filePath , email);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public void ClearTemporaryMemory()
        {
            try
            {
                
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
