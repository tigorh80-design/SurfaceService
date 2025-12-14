using RepositoryLayer.EF;

namespace RepositoryLayer.Repositories
{
    public interface IPostRepository
    {
        Task<List<ForumPostEntity>> GetAllAsync();
        Task<List<ForumPostEntity>> GetByUserAsync(int userId);
        Task<ForumPostEntity?> GetByIdAsync(int id);
        Task<ForumPostEntity> AddAsync(ForumPostEntity entity);
    }
}