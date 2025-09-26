using Railways.Models;
using Railways.Repositories;
using Railways.Domain;


namespace Railways.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repo;

        public CompanyService(ICompanyRepository repo)
        {
            _repo = repo;
        }

        // --- Queries ---
        public async Task<List<Company>> GetAllAsync() =>
            await _repo.GetAllAsync();

        public async Task<Company?> GetByIdAsync(int id) =>
            await _repo.GetByIdAsync(id);

        // --- Lifecycle ---
        public async Task<Company> CreateAsync(string name, int startingMoney, int startingStockPrice)
        {
            var company = new Company
            {
                Name = name,
                Money = startingMoney,
                StockPriceIndex = StockMarket.GetClosestIndex(startingStockPrice),
                TotalShares = 5, // default minor; can be adjusted if needed
                PresidentId = null
            };

            await _repo.AddAsync(company);

            return company;
        }

        public async Task<bool> DeleteAsync(int companyId)
        {
            await _repo.DeleteAsync(companyId);
            return true;
        }

        // --- Game actions ---
        public async Task<bool> PayoutAsync(int companyId, int totalRevenue)
        {
            var company = await _repo.GetByIdAsync(companyId);
            if (company == null) return false;
            
            int stockPrice = StockMarket.Track[company.StockPriceIndex];

            // Adjust stock price based on payout size
            if (totalRevenue >= stockPrice * 2)
            {
                company.StockPriceIndex = StockMarket.MoveUp(company.StockPriceIndex, 2);
            }
            else if (totalRevenue >= stockPrice)
            {
                company.StockPriceIndex = StockMarket.MoveUp(company.StockPriceIndex, 1);
            }

            await _repo.UpdateAsync(company);
            return true;
        }

        public async Task<bool> WithholdAsync(int companyId, int totalRevenue)
        {
            var company = await _repo.GetByIdAsync(companyId);
            if (company == null) return false;

            company.Money += totalRevenue;
            company.StockPriceIndex = StockMarket.MoveDown(company.StockPriceIndex, 1);

            await _repo.UpdateAsync(company);
            return true;
        }

        public async Task<bool> AdjustMoneyAsync(int companyId, int amount)
        {
            var company = await _repo.GetByIdAsync(companyId);
            if (company == null) return false;

            company.Money += amount;

            await _repo.UpdateAsync(company);
            return true;
        }
    }

}