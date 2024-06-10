using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Model
{
    public class Poster
    {
        public int PosterID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string PosterContext { get; set; } = string.Empty;
        public string Intro { get; set; } = string.Empty;

        public Image? ImagePoster { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        [ForeignKey(nameof(Theme))]
        public int ThemeID { get; set; }
        public virtual Theme? Theme { get; set; }
    }
    public class PosterRequest
    {
        public string Title { get; set; } = string.Empty;
        public string PosterContext { get; set; } = string.Empty;
        public string Intro { get; set; } = string.Empty;
        public ImageRequest? ImagePoster { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int ThemeID { get; set; }
      
    }
    public class PosterRequestOnView
    {
        public string Title { get; set; } = string.Empty;
        public string PosterContext { get; set; } = string.Empty;
        public string Intro { get; set; } = string.Empty;
        public ImageRequest? ImagePoster { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int ThemeID { get; set; }
      
    }
    public class PosterRequestUpdate
    {
        public string Title { get; set; } = string.Empty;
        public string PosterContext { get; set; } = string.Empty;
        public string Intro { get; set; } = string.Empty;
        
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int ThemeID { get; set; }
        public ImageRequest? ImagePoster { get; set;}
      
    }
    public class PosterResponseDetail
    {
        public string Title { get; set; } = string.Empty;
        public string PosterContext { get; set; } = string.Empty;
        public string Intro { get; set; } = string.Empty;
        public ImageResponse? ImagePoster { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int ThemeID { get; set; }
    }

    public class PosterResponse
    {
        public int PosterID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Intro { get; set; } = string.Empty;
        public ImageResponse? ImagePoster { get; set; }
        public DateTime UpdateAt { get; set; }

    }
}
