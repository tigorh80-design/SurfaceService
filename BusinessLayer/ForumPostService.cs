using Shared.ForumPosts;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ForumPostService : IForumPostService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ForumPostService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ForumPostResponse>> GetForumPostsAsync()
        {
            var client = _httpClientFactory.CreateClient("JSONPlaceholder");
            var response = await client.GetAsync("posts");
            response.EnsureSuccessStatusCode();
            var forumPosts = await response.Content.ReadFromJsonAsync<List<ForumPostResponse>>();

            return forumPosts ?? new List<ForumPostResponse>();
        }

        public async Task<List<ForumPostResponse>> GetForumPostsByUserAsync(int userId)
        {
            var client = _httpClientFactory.CreateClient("JSONPlaceholder");
            var response = await client.GetAsync($"users/{userId}/posts");
            response.EnsureSuccessStatusCode();
            var forumPosts = await response.Content.ReadFromJsonAsync<List<ForumPostResponse>>();

            return forumPosts ?? new List<ForumPostResponse>();
        }

        public async Task<ForumPostResponse> CreateForumPostAsync(ForumPostRequest forumPostRequest)
        {
            var client = _httpClientFactory.CreateClient("JSONPlaceholder");

            var response = await client.PostAsJsonAsync("posts", forumPostRequest);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ForumPostResponse>();
            return result ?? new ForumPostResponse();
        }
    }
}
