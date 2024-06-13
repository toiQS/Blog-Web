using Blog_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service._auth
{
    public interface IAuthService
    {
        public Task<bool> RegisterUser(RegisterModel registeModel);
        public Task<bool> LoginUser(LoginModel loginModel);
        public Task LogOutUser();
        public Task<string> GennerateTokenString(LoginModel loginModel);
        public Task<bool> RegisterAdmin(RegisterModel registerModel);
    }
}
