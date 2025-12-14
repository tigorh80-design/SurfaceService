
using Shared.ForumPosts;

namespace BusinessLayer
{
    public interface IForumPostService
    {
        Task<List<ForumPostResponse>> GetForumPostsAsync();
        Task<List<ForumPostResponse>> GetForumPostsByUserAsync(int userId);
        Task<List<ForumPostResponse>> GetForumPostsDBByUserAsync(int userId);
        Task<ForumPostResponse> CreateForumPostAsync(ForumPostRequest forumPostRequest);
    }
}