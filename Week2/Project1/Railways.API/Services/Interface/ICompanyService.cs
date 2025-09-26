using Railways.Models;

namespace Railways.Services
{
    public interface ICompanyService
    {
        // Queries
        public Task<List<Company>> GetAllAsync();
        public Task<Company?> GetByIdAsync(int id);

        // Lifecycle
        public Task<Company> CreateAsync(string name, int startingMoney, int startingStockPrice);
        public Task<bool> DeleteAsync(int companyId);

        // Game actions
        public Task<bool> PayoutAsync(int companyId, int totalRevenue);
        public Task<bool> WithholdAsync(int companyId, int totalRevenue);
        public Task<bool> AdjustMoneyAsync(int companyId, int amount);
    }
}