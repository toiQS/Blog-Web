using Blog.Service._profile;
using Blog_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>
        /// Retrieves the profile information.
        /// </summary>
        /// <returns>The profile information.</returns>
        [HttpGet] // Marks this method to respond to HTTP GET requests
        public async Task<IActionResult> GetProfileAsync()
        {
            var result = await _profileService.GetProfileAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Updates the profile information.
        /// </summary>
        /// <param name="request">Profile data to update.</param>
        /// <returns>Result of the update operation.</returns>
        [HttpPut] // Marks this method to respond to HTTP PUT requests
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update(ProfileRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request cannot be null.");
            }

            var result = await _profileService.Update(request);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
