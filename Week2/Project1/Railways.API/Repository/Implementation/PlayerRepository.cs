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

        public async Task<Player?> GetByIdAsync(int id) =>
            await _context.Players
                .Include(p => p.Stocks)
                .ThenInclude(s => s.Company)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Player player)
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Player player)
        {
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
        }
    }
}
