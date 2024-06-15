using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_Model
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }  
        public string UserID { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        [ForeignKey(nameof(Poster))]
        public int PosterID { get; set; }
        public Poster? Poster { get; set; }

        public string CommentContext { get; set; } = string.Empty;

        
        [ForeignKey(nameof(ParentComment))]
        public int? ReplyTo { get; set; }
        public Comment? ParentComment { get; set; } 

        public virtual List<Image>? CommentImage { get; set; } = new List<Image>();
        public virtual List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
