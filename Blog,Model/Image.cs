using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Model
{
    public class Image
    {
        [Key]
        public int ImageID { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty ;
        public string ImageType { get; set; } = string.Empty;
    }
    public class ImageRequest
    {
        
        public string ImageName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty ;
        public string ImageType { get; set; } = string.Empty;
    
    }
    public class ImageRequestOnView
    {

        public string ImageName { get; set; } = string.Empty;
        public IFormFile? ImageUrl { get; set; }
        public string ImageType { get; set; } = string.Empty;

    }
    public class ImageResponse
    {
        public int ImageID { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty ;
    }
    public class ImageResponseDetail
    {
        public int ImageID { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty ;
        public string ImageType { get; set; } = string.Empty;
    }
}
