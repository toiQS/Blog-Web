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
        public int? ProfileID { get; set; }
        public int? PosterID { get; set; }
        public int? CommentID { get; set; }

        public virtual Profile? Profile { get; set; }
        public virtual Poster? Poster { get; set; }
        public virtual Comment? Comment { get; set;}

    }
    public class ImageRequest
    {
        
        public string ImageName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty ;
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
