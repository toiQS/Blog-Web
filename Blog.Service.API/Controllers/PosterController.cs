using Blog.Service._poster;
using Blog_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosterController : ControllerBase
    {
        private readonly IPosterService _posterService;

        /// <summary>
        /// Constructor to initialize the PosterController with the poster service.
        /// </summary>
        /// <param name="posterService">The poster service instance.</param>
        public PosterController(IPosterService posterService)
        {
            _posterService = posterService;
        }

        /// <summary>
        /// Retrieves all posters.
        /// </summary>
        /// <returns>A list of all posters.</returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetPosterAsync()
        {
            var result = await _posterService.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a poster by its ID.
        /// </summary>
        /// <param name="posterID">The ID of the poster to retrieve.</param>
        /// <returns>The details of the specified poster.</returns>
        [HttpGet]
        [Route("{posterID:int}")]
        public async Task<IActionResult> GetPosterByID(int posterID)
        {
            var result = await _posterService.GetDetailByID(posterID);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves posters that match the specified text.
        /// </summary>
        /// <param name="text">The text to search for in the poster.</param>
        /// <returns>A list of posters that match the search text.</returns>
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetPosterByText(string text)
        {
            var result = await _posterService.GetByText(text);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new poster.
        /// </summary>
        /// <param name="poster">The poster request containing the poster information.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPoster([FromBody] PosterRequest poster)
        {
            var result = await _posterService.Create(poster);
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing poster.
        /// </summary>
        /// <param name="posterID">The ID of the poster to update.</param>
        /// <param name="poster">The poster request containing the updated poster information.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        [HttpPut]
        [Route("Update/{posterID:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePoster(int posterID, [FromBody] PosterRequestUpdate poster)
        {
            var result = await _posterService.Update(posterID, poster);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a poster by its ID.
        /// </summary>
        /// <param name="posterID">The ID of the poster to delete.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        [HttpDelete]
        [Route("Delete/{posterID:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DelPoster(int posterID)
        {
            var result = await _posterService.Delete(posterID);
            return Ok(result);
        }
    }
}
