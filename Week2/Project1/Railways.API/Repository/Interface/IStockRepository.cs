using Railways.Models;

namespace Railways.Repositories
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllAsync();
        public Task<Stock?> GetByIdsAsync(int playerId, int companyId);
        public Task<List<Stock>> GetByPlayerIdAsync(int playerId);
        public Task<List<Stock>> GetByCompanyIdAsync(int companyId);
        public Task UpdateAsync(Stock stock);
        public Task AddAsync(Stock stock);
        public Task DeleteAsync(int playerId, int companyId);
    }
}