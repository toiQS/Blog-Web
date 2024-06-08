using Blog.Data;
using Blog_Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Service._image
{
    /// <summary>
    /// Service class for handling image-related operations.
    /// </summary>
    public class ImageService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ImageService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageService"/> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="logger">Logger instance.</param>
        public ImageService(ApplicationDbContext context, ILogger<ImageService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all images from the database.
        /// </summary>
        /// <returns>A service result containing a collection of <see cref="ImageResponse"/>.</returns>
        public async Task<ServiceResult<IEnumerable<ImageResponse>>> GetAll()
        {
            try
            {
                var images = await _context.Images
                    .Select(image => new ImageResponse
                    {
                        ImageID = image.ImageID,
                        ImageName = image.ImageName,
                        ImageUrl = image.ImageUrl,
                    })
                    .ToArrayAsync();

                return images.Any()
                    ? ServiceResult<IEnumerable<ImageResponse>>.SuccessResult(images)
                    : throw new Exception("No images found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all images.");
                return ServiceResult<IEnumerable<ImageResponse>>.FailedResult(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves images by name from the database.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>A service result containing a collection of <see cref="ImageResponse"/>.</returns>
        public async Task<ServiceResult<IEnumerable<ImageResponse>>> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            try
            {
                var images = await _context.Images
                    .Where(image => image.ImageName.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .Select(image => new ImageResponse
                    {
                        ImageID = image.ImageID,
                        ImageName = image.ImageName,
                        ImageUrl = image.ImageUrl,
                    })
                    .ToListAsync();

                return images.Any()
                    ? ServiceResult<IEnumerable<ImageResponse>>.SuccessResult(images)
                    : throw new Exception("No images found with the specified name.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving images by name: {name}.");
                return ServiceResult<IEnumerable<ImageResponse>>.FailedResult(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves an image by its ID from the database.
        /// </summary>
        /// <param name="imageID">The ID of the image.</param>
        /// <returns>A service result containing an <see cref="ImageResponseDetail"/>.</returns>
        public async Task<ServiceResult<ImageResponseDetail>> GetByID(int imageID)
        {
            if (imageID <= 0)
                throw new ArgumentOutOfRangeException(nameof(imageID));

            try
            {
                var image = await _context.Images
                    .Where(image => image.ImageID == imageID)
                    .Select(image => new ImageResponseDetail
                    {
                        ImageID = image.ImageID,
                        ImageName = image.ImageName,
                        ImageUrl = image.ImageUrl,
                        ImageType = image.ImageType
                    })
                    .FirstOrDefaultAsync();

                return image != null
                    ? ServiceResult<ImageResponseDetail>.SuccessResult(image)
                    : throw new Exception("Image not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving image by ID: {imageID}.");
                return ServiceResult<ImageResponseDetail>.FailedResult(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing image in the database.
        /// </summary>
        /// <param name="imageID">The ID of the image to update.</param>
        /// <param name="image">The updated image data.</param>
        /// <returns>A service result indicating the success of the operation.</returns>
        public async Task<ServiceResult<bool>> Update(int imageID, ImageRequest image)
        {
            if (imageID <= 0)
                throw new ArgumentOutOfRangeException(nameof(imageID));

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            try
            {
                var existingImage = await _context.Images
                    .FirstOrDefaultAsync(img => img.ImageID == imageID);

                if (existingImage == null)
                    throw new Exception("Image not found.");

                // Update the properties only if they are different
                if (existingImage.ImageName != image.ImageName)
                {
                    existingImage.ImageName = image.ImageName;
                }

                if (existingImage.ImageUrl != image.ImageUrl)
                {
                    existingImage.ImageUrl = image.ImageUrl;
                }

                if (existingImage.ImageType != image.ImageType)
                {
                    existingImage.ImageType = image.ImageType;
                }

                _context.Images.Update(existingImage);
                await _context.SaveChangesAsync();

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating image with ID: {imageID}.");
                return ServiceResult<bool>.FailedResult(ex.Message);
            }
        }
    }
}
