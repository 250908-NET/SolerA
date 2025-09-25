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

        public async Task<Stock?> GetByIdAsync(int id)
        {
            //return await _context.Stocks.Where( Stock => Stock.id  == id);
            throw new NotImplementedException();
        }

        public async Task AddAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}