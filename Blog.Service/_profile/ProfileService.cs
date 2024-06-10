using Blog.Data;
using Blog_Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Service._profile
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProfileService> _logger;

        public ProfileService(ApplicationDbContext context, ILogger<ProfileService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a single profile.
        /// </summary>
        public async Task<ServiceResult<ProfileResponse>> GetProfileAsync()
        {
            try
            {
                var profile = await _context.Profiles
                    .Where(x => x.ProfileID == 1)
                    .Select(x => new ProfileResponse
                    {
                        FullName = x.FullName,
                        Address = x.Address,
                        DateOfBirth = x.DateOfBirth,
                        Email = x.Email,
                        FaceBook = x.FaceBook,
                        Phone = x.Phone,
                        Reddit = x.Reddit,
                        ProfileImage = new ImageResponse
                        {
                            ImageName = x.ProfileImage.ImageName,
                            ImageID = x.ProfileImage.ImageID,
                            ImageUrl = x.ProfileImage.ImageUrl,
                        }
                    })
                    .FirstOrDefaultAsync();

                if (profile == null)
                {
                    _logger.LogError("Profile with ID {ProfileID} not found.", 1);
                    return ServiceResult<ProfileResponse>.FailedResult("Profile not found.");
                }

                

                return ServiceResult<ProfileResponse>.SuccessResult(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve profile. Exception: {ExceptionMessage}", ex.Message);
                return ServiceResult<ProfileResponse>.FailedResult("Error retrieving profile.");
            }
        }


        /// <summary>
        /// Updates a single profile.
        /// </summary>
        public async Task<ServiceResult<bool>> Update(ProfileRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Profile update request cannot be null.");
            }

            try
            {
                var profile = await _context.Profiles
                    .Include(p => p.ProfileImage)
                    .FirstOrDefaultAsync(p => p.ProfileID == 1);

                if (profile == null)
                {
                    _logger.LogError("Profile to update not found");
                    throw new Exception("Profile to update not found.");
                }

                // Apply changes to the profile
                profile.Reddit = request.Reddit ?? profile.Reddit;
                profile.FullName = request.FullName;
                profile.Address = request.Address;
                profile.Phone = request.Phone;
                profile.DateOfBirth = request.DateOfBirth;
                profile.FaceBook = request.FaceBook;

                if (profile.ProfileImage != null)
                {
                    profile.ProfileImage.ImageName = request.ProfileImage.ImageName;
                    profile.ProfileImage.ImageType = request.ProfileImage.ImageType;
                    profile.ProfileImage.ImageUrl = request.ProfileImage.ImageUrl;
                }
                else
                {
                    // Handle case where there was no image before
                    profile.ProfileImage = new Image
                    {
                        ImageName = request.ProfileImage.ImageName,
                        ImageType = request.ProfileImage.ImageType,
                        ImageUrl = request.ProfileImage.ImageUrl
                    };
                }

                _context.Profiles.Update(profile);
                await _context.SaveChangesAsync();

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating profile.");
                return ServiceResult<bool>.FailedResult(ex.Message);
            }
        }
    }
}
