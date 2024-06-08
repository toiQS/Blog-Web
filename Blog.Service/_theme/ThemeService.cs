using Blog.Data;
using Blog_Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Service._theme
{
    public class ThemeService : IThemeService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ThemeService> _logger;

        /// <summary>
        /// Constructor to initialize the ThemeService with the database context and logger.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logger">The logger instance.</param>
        public ThemeService(ApplicationDbContext context, ILogger<ThemeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all themes.
        /// </summary>
        /// <returns>A service result containing a collection of theme responses.</returns>
        public async Task<ServiceResult<IEnumerable<ThemeResponse>>> GetThemeAsync()
        {
            try
            {
                var getData = await _context.Themes
                    .Select(x => new ThemeResponse()
                    {
                        Info = x.Info,
                        ThemeID = x.ThemeID,
                        ThemeName = x.ThemeName,
                    })
                    .ToArrayAsync();

                if (getData != null)
                {
                    return ServiceResult<IEnumerable<ThemeResponse>>.SuccessResult(getData);
                }
                else throw new Exception("Not Found or Data don't exist");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return ServiceResult<IEnumerable<ThemeResponse>>.FailedResult(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves themes that match the specified text in the theme name.
        /// </summary>
        /// <param name="text">The text to search for in the theme name.</param>
        /// <returns>A service result containing a collection of theme responses.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the text is null or empty.</exception>
        public async Task<ServiceResult<IEnumerable<ThemeResponse>>> GetThemeAsyncByText(string text)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException();

            try
            {
                var getData = await _context.Themes
                    .Where(x => x.ThemeName.ToLower().Contains(text.ToLower()))
                    .Select(x => new ThemeResponse()
                    {
                        Info = x.Info,
                        ThemeID = x.ThemeID,
                        ThemeName = x.ThemeName,
                    })
                    .ToArrayAsync();

                if (getData != null)
                {
                    return ServiceResult<IEnumerable<ThemeResponse>>.SuccessResult(getData);
                }
                else throw new Exception("Not Found or Data don't exist");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return ServiceResult<IEnumerable<ThemeResponse>>.FailedResult(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a theme by its ID.
        /// </summary>
        /// <param name="id">The ID of the theme to retrieve.</param>
        /// <returns>A service result containing the theme response detail.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the ID is less than 0.</exception>
        public async Task<ServiceResult<ThemeResponseDetail>> GetThemeByID(int id)
        {
            if (id < 0) throw new ArgumentOutOfRangeException();

            try
            {
                var getData = await _context.Themes
                    .Where(x => x.ThemeID == id)
                    .FirstOrDefaultAsync();

                if (getData != null)
                {
                    var resultData = new ThemeResponseDetail
                    {
                        Info = getData.Info,
                        ThemeName = getData.ThemeName,
                        Posters = getData.Posters,
                    };
                    return ServiceResult<ThemeResponseDetail>.SuccessResult(resultData);
                }
                else throw new Exception("Not Found or Data don't exist");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return ServiceResult<ThemeResponseDetail>.FailedResult(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new theme.
        /// </summary>
        /// <param name="theme">The theme request containing the theme information.</param>
        /// <returns>A service result indicating success or failure.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the theme name or info is null or empty.</exception>
        public async Task<ServiceResult<bool>> Create(ThemeRequest theme)
        {
            if (string.IsNullOrEmpty(theme.ThemeName) || string.IsNullOrEmpty(theme.Info)) throw new ArgumentNullException();

            try
            {
                var resultData = new Theme
                {
                    ThemeName = theme.ThemeName,
                    Info = theme.Info,
                };

                await _context.Themes.AddAsync(resultData);
                await _context.SaveChangesAsync();

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return ServiceResult<bool>.FailedResult(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a theme by its ID.
        /// </summary>
        /// <param name="id">The ID of the theme to delete.</param>
        /// <returns>A service result indicating success or failure.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the ID is less than 0.</exception>
        public async Task<ServiceResult<bool>> Delete(int id)
        {
            if (id < 0) throw new ArgumentOutOfRangeException();

            try
            {
                var getData = await _context.Themes
                    .Where(x => x.ThemeID == id)
                    .FirstOrDefaultAsync();

                if (getData != null)
                {
                    _context.Themes.Remove(getData);
                    await _context.SaveChangesAsync();
                    return ServiceResult<bool>.SuccessResult(true);
                }
                else throw new Exception("Not Found or Data don't exist");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return ServiceResult<bool>.FailedResult(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing theme.
        /// </summary>
        /// <param name="id">The ID of the theme to update.</param>
        /// <param name="theme">The theme request containing the updated theme information.</param>
        /// <returns>A service result indicating success or failure.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the ID is less than 0 or theme info/name is null or empty.</exception>
        public async Task<ServiceResult<bool>> Update(int id, ThemeRequest theme)
        {
            if (id < 0 || string.IsNullOrEmpty(theme.Info) || string.IsNullOrEmpty(theme.ThemeName))
                throw new ArgumentOutOfRangeException();

            try
            {
                var getData = await _context.Themes
                     .Where(x => x.ThemeID == id)
                     .FirstOrDefaultAsync();

                if (getData != null)
                {
                    if (getData.ThemeName != theme.ThemeName)
                    {
                        getData.ThemeName = theme.ThemeName;
                    }
                    if (getData.Info != theme.Info)
                    {
                        getData.Info = theme.Info;
                    }

                    _context.Themes.Update(getData);
                    await _context.SaveChangesAsync();

                    return ServiceResult<bool>.SuccessResult(true);
                }
                else throw new Exception("Not Found or Data don't exist");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return ServiceResult<bool>.FailedResult(ex.Message);
            }
        }
    }
}
