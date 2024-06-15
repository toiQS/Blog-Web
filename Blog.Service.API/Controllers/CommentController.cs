using Blog.Service._comment;
using Blog_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Service.API.Controllers
{
    /// <summary>
    /// Controller to manage comment-related operations via API.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        /// <summary>
        /// Initializes a new instance of the CommentController class.
        /// </summary>
        /// <param name="commentService">The service to handle comment-related operations.</param>
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Gets all comments.
        /// </summary>
        /// <returns>A list of all comments.</returns>
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var result = await _commentService.GetAll();
            return result.Success ? Ok(result) : StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Gets replies for a specific comment.
        /// </summary>
        /// <param name="id">The ID of the comment to retrieve replies for.</param>
        /// <returns>A list of replies for the specified comment.</returns>
        [HttpGet("replies/{id}")]
        public async Task<IActionResult> GetReplies(int id)
        {
            var result = await _commentService.GetReply(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Gets all top-level comments for a specific post.
        /// </summary>
        /// <param name="postId">The ID of the post to retrieve comments for.</param>
        /// <returns>A list of comments for the specified post.</returns>
        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetCommentsForPost(int postId)
        {
            var result = await _commentService.GetComment(postId);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Gets a specific comment by ID.
        /// </summary>
        /// <param name="id">The ID of the comment to retrieve.</param>
        /// <returns>The comment with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var result = await _commentService.GetByID(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Creates a new comment.
        /// </summary>
        /// <param name="request">The request containing the comment data.</param>
        /// <returns>The result of the comment creation operation.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] CommentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _commentService.CreateComment(request);
            return result.Success ? Ok(result) : StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Creates a reply to an existing comment.
        /// </summary>
        /// <param name="request">The request containing the reply data.</param>
        /// <returns>The result of the reply creation operation.</returns>
        [HttpPost("reply")]
        [Authorize]
        public async Task<IActionResult> AddReply([FromBody] ReplyToRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _commentService.CreateReply(request);
            return result.Success ? Ok(result) : StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Updates an existing comment.
        /// </summary>
        /// <param name="commentID">The ID of the comment to update.</param>
        /// <param name="request">The request containing the updated comment data.</param>
        /// <returns>The result of the comment update operation.</returns>
        [HttpPut("{commentID}")]
        [Authorize]
        public async Task<IActionResult> UpdateComment(int commentID, [FromBody] CommentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _commentService.Update(commentID, request);
            return result.Success ? Ok(result) : StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Deletes a comment by ID.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        /// <returns>The result of the comment deletion operation.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _commentService.Delete(id);
            return result.Success ? Ok(result) : StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}
