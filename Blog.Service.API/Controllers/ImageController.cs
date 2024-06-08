using Blog.Service._image;
using Blog_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        // Constructor to inject the image service dependency
        public ImageController(IImageService imageService)
        {
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
        }

        /// <summary>
        /// Retrieves all images.
        /// </summary>
        /// <returns>A list of all images.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllImages()
        {
            var result = await _imageService.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves images by name.
        /// </summary>
        /// <param name="text">The name or text associated with the image.</param>
        /// <returns>Images matching the given text.</returns>
        [HttpGet("search")]
        public async Task<IActionResult> GetImageByText([FromQuery] string text)
        {
            var result = await _imageService.GetByName(text);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves an image by ID.
        /// </summary>
        /// <param name="id">The ID of the image.</param>
        /// <returns>The image with the specified ID.</returns>
        [HttpGet("{id :int}")]
        public async Task<IActionResult> GetImageByID(int id)
        {
            var result = await _imageService.GetByID(id);
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing image.
        /// </summary>
        /// <param name="imageID">The ID of the image to update.</param>
        /// <param name="imageRequest">The image data to update.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut("{imageID : int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateImage(int imageID, [FromBody] ImageRequest imageRequest)
        {
            var result = await _imageService.Update(imageID, imageRequest);
            return Ok(result);
        }
    }
}
