using Blog.Data;
using Blog_Model;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Blog.Service._comment
{
    /// <summary>
    /// Service class to handle CRUD operations for comments in the blog application.
    /// </summary>
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CommentService> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string filePath = @"F:\Repo\Local\Blog-Web\Blog.Service\_auth\Memory.txt";

        /// <summary>
        /// Initializes a new instance of the CommentService class.
        /// </summary>
        /// <param name="context">The database context for interacting with comments.</param>
        /// <param name="logger">Logger for recording information and errors.</param>
        /// <param name="userManager">UserManager for managing user-related operations.</param>
        public CommentService(ApplicationDbContext context, ILogger<CommentService> logger, UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager;
        }

        /// <summary>
        /// Gets all comments.
        /// </summary>
        /// <returns>A service result containing a list of all comments.</returns>
        public async Task<ServiceResult<IEnumerable<CommentResponse>>> GetAll()
        {
            try
            {
                var comments = await _context.Comments
                    .Select(c => new CommentResponse
                    {
                        CommentID = c.CommentID,
                        UserName = c.UserName,
                        PosterID = c.PosterID,
                        CommentContext = c.CommentContext,
                        ReplyTo = c.ReplyTo
                    })
                    .ToListAsync();

                return comments.Any()
                    ? ServiceResult<IEnumerable<CommentResponse>>.SuccessResult(comments)
                    : ServiceResult<IEnumerable<CommentResponse>>.FailedResult("No comments found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all comments.");
                return ServiceResult<IEnumerable<CommentResponse>>.FailedResult("An error occurred while retrieving comments.");
            }
        }

        /// <summary>
        /// Gets all replies for a specific comment.
        /// </summary>
        /// <param name="id">The ID of the comment to retrieve replies for.</param>
        /// <returns>A service result containing a list of replies for the comment.</returns>
        public async Task<ServiceResult<IEnumerable<CommentResponse>>> GetReply(int id)
        {
            if (id <= 0)
                return ServiceResult<IEnumerable<CommentResponse>>.FailedResult("Invalid comment ID.");

            try
            {
                var replies = await _context.Comments
                    .Where(c => c.ReplyTo == id)
                    .Select(c => new CommentResponse
                    {
                        CommentID = c.CommentID,
                        UserName = c.UserName,
                        PosterID = c.PosterID,
                        CommentContext = c.CommentContext,
                        ReplyTo = c.ReplyTo
                    })
                    .ToListAsync();

                return replies.Any()
                    ? ServiceResult<IEnumerable<CommentResponse>>.SuccessResult(replies)
                    : ServiceResult<IEnumerable<CommentResponse>>.FailedResult("No replies found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving replies.");
                return ServiceResult<IEnumerable<CommentResponse>>.FailedResult("An error occurred while retrieving replies.");
            }
        }

        /// <summary>
        /// Gets all top-level comments for a specific post.
        /// </summary>
        /// <param name="id">The ID of the post to retrieve comments for.</param>
        /// <returns>A service result containing a list of comments for the post.</returns>
        public async Task<ServiceResult<IEnumerable<CommentResponse>>> GetComment(int id)
        {
            if (id <= 0)
                return ServiceResult<IEnumerable<CommentResponse>>.FailedResult("Invalid post ID.");

            try
            {
                var comments = await _context.Comments
                    .Where(c => c.PosterID == id && c.ReplyTo == null)
                    .Select(c => new CommentResponse
                    {
                        CommentID = c.CommentID,
                        UserName = c.UserName,
                        PosterID = c.PosterID,
                        CommentContext = c.CommentContext,
                        ReplyTo = c.ReplyTo
                    })
                    .ToListAsync();

                return comments.Any()
                    ? ServiceResult<IEnumerable<CommentResponse>>.SuccessResult(comments)
                    : ServiceResult<IEnumerable<CommentResponse>>.FailedResult("No comments found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving comments.");
                return ServiceResult<IEnumerable<CommentResponse>>.FailedResult("An error occurred while retrieving comments.");
            }
        }

        /// <summary>
        /// Gets a specific comment by ID.
        /// </summary>
        /// <param name="id">The ID of the comment to retrieve.</param>
        /// <returns>A service result containing the comment if found.</returns>
        public async Task<ServiceResult<CommentResponse>> GetByID(int id)
        {
            if (id <= 0)
                return ServiceResult<CommentResponse>.FailedResult("Invalid comment ID.");

            try
            {
                var comment = await _context.Comments
                    .Where(c => c.CommentID == id)
                    .Select(c => new CommentResponse
                    {
                        CommentID = c.CommentID,
                        UserName = c.UserName,
                        PosterID = c.PosterID,
                        CommentContext = c.CommentContext,
                        ReplyTo = c.ReplyTo
                    })
                    .FirstOrDefaultAsync();

                return comment != null
                    ? ServiceResult<CommentResponse>.SuccessResult(comment)
                    : ServiceResult<CommentResponse>.FailedResult("Comment not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving comment.");
                return ServiceResult<CommentResponse>.FailedResult("An error occurred while retrieving the comment.");
            }
        }

        /// <summary>
        /// Creates a new comment.
        /// </summary>
        /// <param name="comment">The request containing the comment data.</param>
        /// <returns>A service result indicating the success or failure of the operation.</returns>
        public async Task<ServiceResult<bool>> CreateComment(CommentRequest comment)
        {
            if (comment == null)
                return ServiceResult<bool>.FailedResult("Comment data is null.");

            var getKey = File.ReadAllText(filePath);
            var identityUser = await _userManager.FindByEmailAsync(getKey);
            if (identityUser == null)
                return ServiceResult<bool>.FailedResult("User not found.");

            try
            {
                var newComment = new Comment
                {
                    UserID = identityUser.Id,
                    UserName = identityUser.UserName,
                    PosterID = comment.PosterID,
                    CommentContext = comment.CommentContext,
                    ReplyTo = null,
                    CommentImage = comment.CommentImage?.Select(i => new Image
                    {
                        ImageType = i.ImageType,
                        ImageName = i.ImageName,
                        ImageUrl = i.ImageUrl
                    }).ToList()
                };

                await _context.Comments.AddAsync(newComment);
                await _context.SaveChangesAsync();

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating new comment: {ex.Message}");
                return ServiceResult<bool>.FailedResult($"An error occurred while creating the comment. Details: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Creates a reply to an existing comment.
        /// </summary>
        /// <param name="comment">The request containing the reply data.</param>
        /// <returns>A service result indicating the success or failure of the operation.</returns>
        public async Task<ServiceResult<bool>> CreateReply(ReplyToRequest comment)
        {
            if (comment == null)
                return ServiceResult<bool>.FailedResult("Reply data is null.");

            var getKey = File.ReadAllText(filePath);
            var identityUser = await _userManager.FindByEmailAsync(getKey);
            if (identityUser == null)
                return ServiceResult<bool>.FailedResult("User not found.");

            try
            {
                var newReply = new Comment
                {
                    UserID = identityUser.Id,
                    UserName = identityUser.UserName,
                    PosterID = comment.PosterID,
                    CommentContext = comment.CommentContext,
                    ReplyTo = comment.ReplyTo,
                    CommentImage = comment.CommentImage?.Select(i => new Image
                    {
                        ImageType = i.ImageType,
                        ImageName = i.ImageName,
                        ImageUrl = i.ImageUrl
                    }).ToList()
                };

                await _context.Comments.AddAsync(newReply);
                await _context.SaveChangesAsync();

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating reply: {ex.Message}");
                return ServiceResult<bool>.FailedResult($"An error occurred while creating the reply. Details: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Updates an existing comment.
        /// </summary>
        /// <param name="commentID">The ID of the comment to update.</param>
        /// <param name="comment">The request containing the updated comment data.</param>
        /// <returns>A service result indicating the success or failure of the operation.</returns>
        public async Task<ServiceResult<bool>> Update(int commentID, CommentRequest comment)
        {
            if (comment == null)
                return ServiceResult<bool>.FailedResult("Comment data is null.");
            if (commentID <= 0)
                return ServiceResult<bool>.FailedResult("Invalid comment ID.");

            // Ensure the method of retrieving user key is secure and correct
            var getKey = File.ReadAllText(filePath);
            var identityUser = await _userManager.FindByEmailAsync(getKey);
            if (identityUser == null)
                return ServiceResult<bool>.FailedResult("User not found.");

            try
            {
                var existingComment = await _context.Comments.FindAsync(commentID);
                if (existingComment == null)
                    return ServiceResult<bool>.FailedResult("Comment not found.");
                if (existingComment.UserID != identityUser.Id) return ServiceResult<bool>.FailedResult("Not authorized to update this comment.");
                existingComment.CommentContext = comment.CommentContext;
                existingComment.CommentImage = comment.CommentImage?.Select(i => new Image
                {
                    ImageType = i.ImageType,
                    ImageName = i.ImageName,
                    ImageUrl = i.ImageUrl
                }).ToList();

                _context.Comments.Update(existingComment);
                await _context.SaveChangesAsync();

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating comment: {ex.Message}");
                return ServiceResult<bool>.FailedResult($"An error occurred while updating the comment. Details: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Deletes a comment by ID.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        /// <returns>A service result indicating the success or failure of the operation.</returns>
        public async Task<ServiceResult<bool>> Delete(int id)
        {
            if (id <= 0)
                return ServiceResult<bool>.FailedResult("Invalid comment ID.");

            // Ensure the method of retrieving user key is secure and correct
            var getKey = File.ReadAllText(filePath);
            var identityUser = await _userManager.FindByEmailAsync(getKey);

            if (identityUser == null)
                return ServiceResult<bool>.FailedResult("User not found.");

            try
            {
                var comment = await _context.Comments.
                    Where(x => x.CommentID == id)
                    .Include(x => x.CommentImage)
                    .Include(x => x.Comments)
                    .FirstOrDefaultAsync();
                if (comment == null)
                    return ServiceResult<bool>.FailedResult("Comment not found.");
                if(comment.UserID != identityUser.Id) return ServiceResult<bool>.FailedResult("Not authorized to update this comment.");
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting comment: {ex.Message}");
                return ServiceResult<bool>.FailedResult($"An error occurred while deleting the comment. Details: {ex.StackTrace}");
            }
        }
    }
}
