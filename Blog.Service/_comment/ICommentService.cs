using Blog_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service._comment
{
    public interface ICommentService
    {
        public Task<ServiceResult<IEnumerable<CommentResponse>>> GetAll();
        public Task<ServiceResult<IEnumerable<CommentResponse>>> GetReply(int id);
        public Task<ServiceResult<IEnumerable<CommentResponse>>> GetComment(int id);
        public Task<ServiceResult<CommentResponse>> GetByID(int id);
        public Task<ServiceResult<bool>> Delete(int id);
        public Task<ServiceResult<bool>> CreateComment(CommentRequest comment);
        public Task<ServiceResult<bool>> CreateReply(ReplyToRequest comment);
        public Task<ServiceResult<bool>> Update(int commentID, CommentRequest comment);
    }
}
