using Blog.Service._theme;
using Blog_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IThemeService _service;

        /// <summary>
        /// Constructor to initialize the ThemeController with the theme service.
        /// </summary>
        /// <param name="service">The theme service instance.</param>
        public ThemeController(IThemeService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all themes.
        /// </summary>
        /// <returns>A list of theme responses.</returns>
        [HttpGet]
        [Route("GetThemes")]
        public async Task<IActionResult> GetThemeAsync()
        {
            var result = await _service.GetThemeAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves themes that match the specified text in the theme name.
        /// </summary>
        /// <param name="text">The text to search for in the theme name.</param>
        /// <returns>A list of theme responses.</returns>
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetThemeAsyncByText(string text)
        {
            var result = await _service.GetThemeAsyncByText(text);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a theme by its ID.
        /// </summary>
        /// <param name="id">The ID of the theme to retrieve.</param>
        /// <returns>The theme response detail.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetThemeByID(int id)
        {
            var result = await _service.GetThemeByID(id);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new theme.
        /// </summary>
        /// <param name="newTheme">The theme request containing the theme information.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTheme(ThemeRequest newTheme)
        {
            var result = await _service.Create(newTheme);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a theme by its ID.
        /// </summary>
        /// <param name="id">The ID of the theme to delete.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        [HttpDelete]
        [Route("Delete/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DelTheme(int id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing theme.
        /// </summary>
        /// <param name="id">The ID of the theme to update.</param>
        /// <param name="theme">The theme request containing the updated theme information.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        [HttpPut]
        [Route("Update/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTheme(int id, ThemeRequest theme)
        {
            var result = await _service.Update(id, theme);
            return Ok(result);
        }
    }
}
