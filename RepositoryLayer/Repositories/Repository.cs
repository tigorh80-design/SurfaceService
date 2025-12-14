using RepositoryLayer.EF;
using Microsoft.EntityFrameworkCore;


namespace RepositoryLayer.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly RepoDbContext _ctx;

        public PostRepository(RepoDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<ForumPostEntity>> GetAllAsync() =>
            await _ctx.ForumPosts.AsNoTracking().ToListAsync();

        public async Task<List<ForumPostEntity>> GetByUserAsync(int userId) =>
            await _ctx.ForumPosts.AsNoTracking()
                                 .Where(p => p.UserId == userId)
                                 .ToListAsync();

        public async Task<ForumPostEntity?> GetByIdAsync(int id) =>
            await _ctx.ForumPosts.FindAsync(id);

        public async Task<ForumPostEntity> AddAsync(ForumPostEntity entity)
        {
            var entry = (await _ctx.ForumPosts.AddAsync(entity)).Entity;
            await _ctx.SaveChangesAsync();
            return entry;
        }
    }
}