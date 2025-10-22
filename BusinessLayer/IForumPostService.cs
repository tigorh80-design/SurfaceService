
using Shared.ForumPosts;

namespace BusinessLayer
{
    public interface IForumPostService
    {
        Task<List<ForumPost>> GetForumPostsAsync();
        Task<List<ForumPost>> GetForumPostsByUserAsync(int userId);
    }
}