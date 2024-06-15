using Blog.Data;
using Blog_Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Service._poster
{
    public class PosterService : IPosterService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PosterService> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public PosterService(ApplicationDbContext context, ILogger<PosterService> logger, UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Retrieves all posters.
        /// </summary>
        /// <returns>A ServiceResult containing a list of all posters.</returns>
        public async Task<ServiceResult<IEnumerable<PosterResponse>>> GetAll()
        {
            try
            {
                var posters = await _context.Posters
                    .AsNoTracking()
                    .Select(x => new PosterResponse
                    {
                        Intro = x.Intro,
                        PosterID = x.PosterID,
                        Title = x.Title,
                        UpdateAt = x.UpdateAt,
                        ImagePoster = new ImageResponse
                        {
                            ImageName = x.ImagePoster.ImageName,
                            ImageUrl = x.ImagePoster.ImageUrl,
                            ImageID = x.ImagePoster.ImageID,
                        },
                        UserName = x.UserName
                    })
                    .ToListAsync();

                return posters.Any()
                    ? ServiceResult<IEnumerable<PosterResponse>>.SuccessResult(posters)
                    : ServiceResult<IEnumerable<PosterResponse>>.FailedResult("No posters found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all posters.");
                return ServiceResult<IEnumerable<PosterResponse>>.FailedResult("An error occurred while retrieving posters.");
            }
        }

        /// <summary>
        /// Retrieves posters that match the given text.
        /// </summary>
        /// <param name="text">The text to search for.</param>
        /// <returns>A ServiceResult containing a list of posters matching the text.</returns>
        public async Task<ServiceResult<IEnumerable<PosterResponse>>> GetByText(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            try
            {
                var posters = await _context.Posters
                    .AsNoTracking()
                    .Where(x => x.Title.ToLower().Contains(text.ToLower()))
                    .Select(x => new PosterResponse
                    {
                        Intro = x.Intro,
                        PosterID = x.PosterID,
                        Title = x.Title,
                        UpdateAt = x.UpdateAt,
                        ImagePoster = new ImageResponse
                        {
                            ImageName = x.ImagePoster.ImageName,
                            ImageUrl = x.ImagePoster.ImageUrl,
                            ImageID = x.ImagePoster.ImageID,
                        },
                        UserName = x.UserName
                    })
                    .ToListAsync();

                return posters.Any()
                    ? ServiceResult<IEnumerable<PosterResponse>>.SuccessResult(posters)
                    : ServiceResult<IEnumerable<PosterResponse>>.FailedResult("No matching posters found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving posters by text.");
                return ServiceResult<IEnumerable<PosterResponse>>.FailedResult("An error occurred while retrieving posters.");
            }
        }

        /// <summary>
        /// Retrieves the details of a specific poster by ID.
        /// </summary>
        /// <param name="posterID">The ID of the poster.</param>
        /// <returns>A ServiceResult containing the details of the poster.</returns>
        public async Task<ServiceResult<PosterResponseDetail>> GetDetailByID(int posterID)
        {
            if (posterID <= 0)
                throw new ArgumentOutOfRangeException(nameof(posterID));

            try
            {
                var poster = await _context.Posters
                    .AsNoTracking()
                    .Where(x => x.PosterID == posterID)
                    .Select(x => new PosterResponseDetail
                    {
                        Intro = x.Intro,
                        PosterContext = x.PosterContext,
                        Title = x.Title,
                        UpdateAt = x.UpdateAt,
                        ImagePoster = new ImageResponse
                        {
                            ImageName = x.ImagePoster.ImageName,
                            ImageUrl = x.ImagePoster.ImageUrl,
                            ImageID = x.ImagePoster.ImageID,
                        },
                        CreateAt = x.CreateAt,
                        ThemeID = x.ThemeID,
                        UserName = x.UserName
                    })
                    .FirstOrDefaultAsync();

                return poster != null
                    ? ServiceResult<PosterResponseDetail>.SuccessResult(poster)
                    : ServiceResult<PosterResponseDetail>.FailedResult("Poster not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving poster details by ID.");
                return ServiceResult<PosterResponseDetail>.FailedResult("An error occurred while retrieving poster details.");
            }
        }

        /// <summary>
        /// Creates a new poster.
        /// </summary>
        /// <param name="poster">The poster data.</param>
        /// <returns>A ServiceResult indicating whether the poster was created successfully.</returns>
        public async Task<ServiceResult<bool>> Create(PosterRequest poster)
        {
            if (poster == null)
                throw new ArgumentNullException(nameof(poster));

            try
            {
                var getKey = File.ReadAllText("F:\\Repo\\Local\\Blog-Web\\Blog.Service\\_auth\\Memory.txt");
                var identityUser = await _userManager.FindByEmailAsync(getKey);
                if (identityUser == null)
                    throw new Exception("User not found.");

                var newPoster = new Poster
                {
                    Intro = poster.Intro,
                    PosterContext = poster.PosterContext,
                    Title = poster.Title,
                    UpdateAt = poster.UpdateAt,
                    CreateAt = poster.CreateAt,
                    ThemeID = poster.ThemeID,
                    ImagePoster = new Image
                    {
                        ImageType = poster.ImagePoster.ImageType,
                        ImageName = poster.ImagePoster.ImageName,
                        ImageUrl = poster.ImagePoster.ImageUrl,
                    },
                    UserID = identityUser.Id,
                    UserName = identityUser.UserName
                };

                await _context.Posters.AddAsync(newPoster);
                await _context.SaveChangesAsync();

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new poster.");
                return ServiceResult<bool>.FailedResult("An error occurred while creating the poster.");
            }
        }

        /// <summary>
        /// Updates an existing poster.
        /// </summary>
        /// <param name="posterID">The ID of the poster to update.</param>
        /// <param name="poster">The updated poster data.</param>
        /// <returns>A ServiceResult indicating whether the poster was updated successfully.</returns>
        public async Task<ServiceResult<bool>> Update(int posterID, PosterRequestUpdate poster)
        {
            if (posterID <= 0)
                throw new ArgumentOutOfRangeException(nameof(posterID));
            if (poster == null)
                throw new ArgumentNullException(nameof(poster));

            try
            {
                var getKey = File.ReadAllText("F:\\Repo\\Local\\Blog-Web\\Blog.Service\\_auth\\Memory.txt");
                var existingPoster = await _context.Posters
                    .Include(x => x.ImagePoster)
                    .FirstOrDefaultAsync(x => x.PosterID == posterID);

                if (existingPoster == null)
                    return ServiceResult<bool>.FailedResult("Poster not found.");

                if (existingPoster.UserID != getKey)
                    return ServiceResult<bool>.FailedResult("Not authorized to update this poster.");

                // Update properties if they are different
                existingPoster.ThemeID = poster.ThemeID;
                existingPoster.PosterContext = poster.PosterContext;
                existingPoster.Title = poster.Title;
                existingPoster.Intro = poster.Intro;
                existingPoster.UpdateAt = DateTime.Now;
                existingPoster.ImagePoster.ImageUrl = poster.ImagePoster.ImageUrl;
                existingPoster.ImagePoster.ImageName = poster.ImagePoster.ImageName;
                existingPoster.ImagePoster.ImageType = poster.ImagePoster.ImageType;

                _context.Posters.Update(existingPoster);
                await _context.SaveChangesAsync();

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating poster.");
                return ServiceResult<bool>.FailedResult("An error occurred while updating the poster.");
            }
        }

        /// <summary>
        /// Deletes a poster by ID.
        /// </summary>
        /// <param name="posterID">The ID of the poster to delete.</param>
        /// <returns>A ServiceResult indicating whether the poster was deleted successfully.</returns>
        public async Task<ServiceResult<bool>> Delete(int posterID)
        {
            if (posterID <= 0)
                throw new ArgumentOutOfRangeException(nameof(posterID));

            try
            {
                var getKey = File.ReadAllText("F:\\Repo\\Local\\Blog-Web\\Blog.Service\\_auth\\Memory.txt");
                var poster = await _context.Posters
                    .Include(x => x.ImagePoster)
                    .FirstOrDefaultAsync(x => x.PosterID == posterID);

                if (poster == null)
                    return ServiceResult<bool>.FailedResult("Poster not found.");

                if (poster.UserID != getKey)
                    return ServiceResult<bool>.FailedResult("Not authorized to delete this poster.");

                _context.Posters.Remove(poster);
                await _context.SaveChangesAsync();

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting poster.");
                return ServiceResult<bool>.FailedResult("An error occurred while deleting the poster.");
            }
        }
    }
}
