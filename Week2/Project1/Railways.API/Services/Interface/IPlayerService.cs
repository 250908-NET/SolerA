using Railways.Models;

namespace Railways.Services
{
    public interface IPlayerService
    {
        public Task<List<Player>> GetAllASync();
        public Task<Player?> GetByIdAsync(int id);
        public Task CreateAsync(Player player);
    }

}