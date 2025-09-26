using Railways.Models;

namespace Railways.Services
{
    public interface IPlayerService
    {
        public Task<Player?> GetByIdAsync(int id);
        public Task<List<Player>> GetAllAsync();
        public Task<Player> CreateAsync(string username, int startingMoney);
        public Task<bool> DeleteAsync(int id);

        // Game actions
        public Task<bool> BuySharesAsync(int playerId, int companyId, int shares);
        public Task<bool> SellSharesAsync(int playerId, int companyId, int shares);
        public Task<bool> IPOAsync(int playerId, int companyId, int initialStockPrice);
        public Task<Company?> MergeCompaniesAsync(int playerId, int companyId1, int companyId2, string majorCompanyName);
    }
}
