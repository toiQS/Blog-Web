using Blog_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service._profile
{
    public interface IProfileService
    {
        public Task<ServiceResult<ProfileResponse>> GetProfileAsync();
        public Task<ServiceResult<bool>> Update(ProfileRequest request);

    }
}
