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

        public async Task<List<ForumPost>> GetForumPostsAsync()
        {
            var client = _httpClientFactory.CreateClient("JSONPlaceholder");
            var response = await client.GetAsync("posts");
            response.EnsureSuccessStatusCode();
            //return await response.Content.ReadAsStringAsync();
            var forumPosts = await response.Content.ReadFromJsonAsync<List<ForumPost>>();

            return forumPosts;
        }
    }
}
