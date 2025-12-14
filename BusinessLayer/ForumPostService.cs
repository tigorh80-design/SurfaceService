using RepositoryLayer.Repositories;
using Shared.ForumPosts;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ForumPostService : IForumPostService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPostRepository _postRepository;

        public ForumPostService(IHttpClientFactory httpClientFactory, IPostRepository postRepository)
        {
            _httpClientFactory = httpClientFactory;
            _postRepository = postRepository;
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

            var postsFromDb = await _postRepository.GetByUserAsync(userId);

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
