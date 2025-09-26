using Railways.Models;
using Railways.Data;
using Microsoft.EntityFrameworkCore;

namespace Railways.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly RailwaysDbContext _context;

        public StockRepository(RailwaysDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
           return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdsAsync(int playerId, int companyId) =>
            await _context.Stocks
                .FirstOrDefaultAsync(s => s.PlayerId == playerId && s.CompanyId == companyId);

        public async Task<List<Stock>> GetByPlayerIdAsync(int playerId) =>
            await _context.Stocks
                .Include(s => s.Company)
                .Where(s => s.PlayerId == playerId)
                .ToListAsync();
        
        public async Task<List<Stock>> GetByCompanyIdAsync(int companyId) =>
            await _context.Stocks
                .Include(s => s.Player)
                .Where(s => s.CompanyId == companyId)
                .ToListAsync();
        public async Task UpdateAsync(Stock stock)
        {
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();
        }
        public async Task AddAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int playerId, int companyId)
        {
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s => s.PlayerId == playerId && s.CompanyId == companyId);

            if (stock != null)
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }
    }
}