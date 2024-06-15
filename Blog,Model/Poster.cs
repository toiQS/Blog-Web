using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string UserID { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public List<Comment> CommentList { get; set; } = new List<Comment>();
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

    public class PosterRequestUpdate
    {
        public string Title { get; set; } = string.Empty;
        public string PosterContext { get; set; } = string.Empty;
        public string Intro { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int ThemeID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public ImageRequest? ImagePoster { get; set; }
    }

    public class PosterResponseDetail
    {
        public string Title { get; set; } = string.Empty;
        public string PosterContext { get; set; } = string.Empty;
        public string Intro { get; set; } = string.Empty;
        public ImageResponse? ImagePoster { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int ThemeID { get; set; }
    }

    public class PosterResponse
    {
        public int PosterID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Intro { get; set; } = string.Empty;
        public ImageResponse? ImagePoster { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
