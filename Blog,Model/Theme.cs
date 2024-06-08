using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Model
{
    public class Theme
    {
        [Key]
        public int ThemeID { get; set; }
        public string ThemeName { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public IEnumerable<Poster> Posters { get; set; } = new List<Poster>();
    }
    public class ThemeResponse
    {
        [Key]
        public int ThemeID { get; set; }
        public string ThemeName { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        
    }
    public class ThemeResponseDetail
    {
        
        public string ThemeName { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public IEnumerable<Poster> Posters { get; set; } = new List<Poster>();
    }
    
    public class ThemeRequest
    {
        
        
        public string ThemeName { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
    }
    
}
