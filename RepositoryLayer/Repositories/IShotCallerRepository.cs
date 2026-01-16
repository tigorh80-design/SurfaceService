using RepositoryLayer.EF;
using Shared.ShotCaller;

namespace RepositoryLayer.Repositories
{
    public interface IShotCallerRepository
    {
        
        //Task<List<ForumPostEntity>> GetByUserAsync(int userId);
        //Task<ForumPostEntity?> GetByIdAsync(int id);
        Task<ShotCallerEntity> AddAsync(ShotCallerRequest shotCallerRequest);
        Task<List<ShotCallerEntity>> GetAllAsync();
    }
}