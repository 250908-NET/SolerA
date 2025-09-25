using Railways.Models;
using Railways.Data;
using Microsoft.EntityFrameworkCore;

namespace Railways.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly RailwaysDbContext _context;

        public PlayerRepository(RailwaysDbContext context)
        {
            _context = context;
        }

        public async Task<List<Player>> GetAllAsync()
        {
           return await _context.Players.ToListAsync();
        }

        public async Task<Player?> GetByIdAsync(int id)
        {
            //return await _context.Players.Where( Player => Player.id  == id);
            throw new NotImplementedException();
        }

        public async Task AddAsync(Player player)
        {
            await _context.Players.AddAsync(player);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
