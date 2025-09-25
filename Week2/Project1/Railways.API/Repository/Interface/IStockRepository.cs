using Railways.Models;

namespace Railways.Repositories
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllAsync();
        public Task<Stock?> GetByIdAsync(int id);
        public Task AddAsync(Stock stock);
        public Task SaveChangesAsync();
    }
}