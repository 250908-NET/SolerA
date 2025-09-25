using Railways.Models;

namespace Railways.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepoository _repo;
        public PlayerService()
        {
            _repo = _repo;
        }

        public async Task<List<Player>> GetAllASync()
        {
            await _repo.GetAllAsync();
        }
        public async Task<Player?> GetByIdAsync(int id)
        {
            await _repo.GetByIdAsync(id);
        }
        public async Task CreateAsync(Player player)
        {
            await _repo.AddAsync(player);
            await _repo.SaveChangesAsync();
        }
    }

}