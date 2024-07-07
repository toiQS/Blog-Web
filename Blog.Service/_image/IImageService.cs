using Blog_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service._image
{
    public interface IImageService
    {
        public Task<ServiceResult<IEnumerable<ImageResponse>>> GetAll();
        public Task<ServiceResult<IEnumerable<ImageResponse>>> GetByName(string name);
        public Task<ServiceResult<ImageResponseDetail>> GetByID(int imageID);
        public Task<ServiceResult<bool>> Update(int imageID, ImageRequest image);
    }
}
