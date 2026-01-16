using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryLayer.EF;
using Shared.ShotCaller;

namespace RepositoryLayer.Repositories
{
    public class ShotCallerRepository : IShotCallerRepository
    {
        private readonly RepoDbContext _ctx;
        private readonly ILogger<ShotCallerRepository> _logger;

        public ShotCallerRepository(ILogger<ShotCallerRepository> logger, RepoDbContext ctx)
        {
            _ctx = ctx;
            _logger = logger;
        }

        //public async Task<List<ForumPostEntity>> GetAllAsync() =>
        //    await _ctx.ForumPosts.AsNoTracking().ToListAsync();

        //public async Task<List<ForumPostEntity>> GetByUserAsync(int userId) =>
        //    await _ctx.ForumPosts.AsNoTracking()
        //                         .Where(p => p.UserId == userId)
        //                         .ToListAsync();

        //public async Task<ForumPostEntity?> GetByIdAsync(int id) =>
        //    await _ctx.ForumPosts.FindAsync(id);

        public async Task<ShotCallerEntity> AddAsync(ShotCallerRequest shotCallerRequest)
        {
            var entity = new ShotCallerEntity 
            { 
                Name = shotCallerRequest.Name, 
                Tequila = shotCallerRequest.Tequila, 
                Vodka = shotCallerRequest.Vodka, 
                Beers = shotCallerRequest.Beers
            }; 
            _ctx.ShotCaller.Add(entity);
            await _ctx.SaveChangesAsync();
        
            return entity;
        }
    }
}