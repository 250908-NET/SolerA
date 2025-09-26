using Railways.Models;

namespace Railways.Services
{
    public interface IStockService
    {
        public Task<List<Stock>> GetAllAsync();
        public Task<Stock?> GetByIdsAsync(int playerId, int companyId);
        public Task<List<Stock>> GetByPlayerIdAsync(int playerId);
        public Task<List<Stock>> GetByCompanyIdAsync(int companyId);
        public Task<bool> AddOrUpdateAsync(int playerId, int companyId, int sharesOwned);
        public Task<bool> DeleteAsync(int playerId, int companyId);
    }
}