using Blog_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service._poste
{
    public interface IPosterService
    {
        public Task<ServiceResult<IEnumerable<PosterResponse>>> GetAll();
        public Task<ServiceResult<IEnumerable<PosterResponse>>> GetByText(string text);
        public Task<ServiceResult<PosterResponseDetail>> GetDetailByID(int posterID);
        public Task<ServiceResult<bool>> Create(PosterRequest poster);
        public Task<ServiceResult<bool>> Update(int posterID, PosterRequestUpdate poster);
        public Task<ServiceResult<bool>> Delete(int posterID);
    }
}
