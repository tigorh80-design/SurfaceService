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

        [HttpPost("forumPosts")]
        public async Task<List<ForumPost>> GetProductsAsync()
        {
            var forumPosts = await _forumPostService.GetForumPostsAsync();

            return forumPosts;
        }
    }
}
