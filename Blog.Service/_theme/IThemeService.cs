using Blog_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service._theme
{
    public interface IThemeService
    {
        public Task<ServiceResult<IEnumerable<ThemeResponse>>> GetThemeAsync();
        public Task<ServiceResult<IEnumerable<ThemeResponse>>> GetThemeAsyncByText(string text);
        public Task<ServiceResult<ThemeResponseDetail>> GetThemeByID(int id);
        public Task<ServiceResult<bool>> Create(ThemeRequest theme);
        public Task<ServiceResult<bool>> Delete(int id);
        public Task<ServiceResult<bool>> Update(int id, ThemeRequest theme);
    }
}
