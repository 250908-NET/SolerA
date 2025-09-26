using Railways.Models;

namespace Railways.Repositories
{
    public interface IPlayerRepository
    {
        public Task<List<Player>> GetAllAsync();
        public Task<Player?> GetByIdAsync(int id);
        public Task AddAsync(Player player);
        public Task UpdateAsync(Player player);
        public Task DeleteAsync(int id);
    }
}