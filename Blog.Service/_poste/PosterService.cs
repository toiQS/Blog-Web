using Blog.Data;
using Blog_Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service._poste
{
    public class PosterService : IPosterService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PosterService> _logger;
        public PosterService(ApplicationDbContext context, ILogger<PosterService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<ServiceResult<IEnumerable<PosterResponse>>> GetAll()
        {
            try
            {
                var getData = await _context.Posters
                    .Select(x => new PosterResponse()
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
                        }
                    })
                    .ToArrayAsync();
                if (getData != null)
                {
                    return ServiceResult<IEnumerable<PosterResponse>>.SuccessResult(getData);
                }
                throw new Exception("Not Found or Data don't exist");
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<PosterResponse>>.FailedResult(ex.Message);
            }
        }
        public async Task<ServiceResult<IEnumerable<PosterResponse>>> GetByText(string text)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(text);
            try
            {
                var getData = await _context.Posters
                    .Where(x => x.Title.ToLower().Contains(text.ToLower()))
                    .Select(x => new PosterResponse()
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
                        }
                    })
                    .ToListAsync();
                if (getData != null)
                {
                    return ServiceResult<IEnumerable<PosterResponse>>.SuccessResult(getData);
                }
                throw new Exception("Not Found or Data don't exist");
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<PosterResponse>>.FailedResult(ex.Message);
            }
        }
        public async Task<ServiceResult<PosterResponseDetail>> GetDetailByID(int posterID)
        {
            if (posterID < 0) throw new ArgumentNullException();
            try
            {
                var getData = await _context.Posters
                    .Where(x => x.PosterID == posterID)
                    .Select(x => new PosterResponseDetail()
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
                    })
                    .FirstOrDefaultAsync();
                if (getData != null)
                {
                    return ServiceResult<PosterResponseDetail>.SuccessResult(getData);
                }
                throw new Exception("Not Found or Data don't exist");

            }
            catch (Exception ex)
            {
                return ServiceResult<PosterResponseDetail>.FailedResult(ex.Message);
            }
        }
        public async Task<ServiceResult<bool>> Create(PosterRequest poster)
        {
            var checkInput = poster.ToString();
            if (string.IsNullOrEmpty(checkInput)) throw new ArgumentNullException();
            var broken = checkInput.Split(",");
            foreach (var item in broken)
            {
                if (string.IsNullOrEmpty(item) || item == "0")
                    throw new Exception();
            }

            var convertData = new Poster
            {
                Intro = poster.Intro,
                PosterContext = poster.PosterContext,
                Title = poster.Title,
                UpdateAt = poster.UpdateAt,
                CreateAt = poster.CreateAt,
                ThemeID = poster.ThemeID,
                ImagePoster = new Image()
                {
                    ImageType = poster.ImagePoster.ImageType,
                    ImageName = poster.ImagePoster.ImageName,
                    ImageUrl = poster.ImagePoster.ImageUrl,
                }
            };
            try
            {
                await _context.Posters.AddAsync(convertData);
                await _context.SaveChangesAsync();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailedResult(ex.Message);
            }
        }
        public async Task<ServiceResult<bool>> Update(int posterID, PosterRequestUpdate poster)
        {
            var checkInput = poster.ToString();
            if (string.IsNullOrEmpty(checkInput)) throw new ArgumentNullException();
            var broken = checkInput.Split(",");
            foreach (var item in broken)
            {
                if (string.IsNullOrEmpty(item) || item == "0")
                    throw new Exception();
            }
            if (posterID < 0) throw new ArgumentOutOfRangeException();
            try
            {
                var getData = await _context.Posters
                    .Where(x => x.PosterID == posterID)
                    .FirstOrDefaultAsync();


                if (getData != null)
                {
                    

                    if (getData.ThemeID != poster.ThemeID)
                    {
                        getData.ThemeID = poster.ThemeID;
                    }

                    if (getData.PosterContext != poster.PosterContext)
                    {
                        getData.PosterContext = poster.PosterContext;
                    }

                    if (getData.Title != poster.Title)
                    {
                        getData.Title = poster.Title;
                    }

                    if (getData.Intro != poster.Intro)
                    {
                        getData.Intro = poster.Intro;
                    }

                    getData.UpdateAt = DateTime.Now;
                    _context.Posters.Update(getData);
                    
                    await _context.SaveChangesAsync();

                    return ServiceResult<bool>.SuccessResult(true);
                }
                throw new Exception("Not Found or Data don't exist");
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailedResult(ex.Message);
            }
        }
        public async Task<ServiceResult<bool>> Delete(int posterID)
        {
            if (posterID < 0) throw new ArgumentOutOfRangeException();
            try
            {
                var getData = await _context.Posters
                    .Where(x => x.PosterID == posterID)
                    .Include(x =>x.ImagePoster)
                    .FirstOrDefaultAsync();

                if (getData != null)
                {
                    _context.Posters .Remove(getData);
                    await _context.SaveChangesAsync();

                    return ServiceResult<bool>.SuccessResult(true);
                }
                throw new Exception("Not Found or Data don't exist");
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailedResult(ex.Message);
            }
        }
    }
}
