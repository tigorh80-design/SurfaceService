using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Shared.ForumPosts;

namespace SurfaceServiceServer.Controllers
{
    [ApiController]
    [Route("JsonPlaceholder")]
    public class JsonPlaceholderController : ControllerBase
    {
        private readonly ILogger<JsonPlaceholderController> _logger;
        private IForumPostService _forumPostService;

        public JsonPlaceholderController(ILogger<JsonPlaceholderController> logger, IForumPostService forumPostService)
        {
            _logger = logger;
            _forumPostService = forumPostService;
        }
        /// <summary>
        /// Get all forum posts
        /// </summary>
        /// <returns></returns>
        [HttpGet("forumPosts")]
        public async Task<List<ForumPost>> GetForumPostsAsync()
        {
            var forumPosts = await _forumPostService.GetForumPostsAsync();

            return forumPosts;
        }

        /// <summary>
        /// Get forum posts by user id  
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("forumPosts/users/{userId}")]
        public async Task<List<ForumPost>> GetForumPostsByUserAsync(int userId)
        {
            var forumPosts = await _forumPostService.GetForumPostsByUserAsync(userId);

            return forumPosts;
        }

    }
}
