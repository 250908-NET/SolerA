using Railways.Models;
using Railways.Repositories;

namespace Railways.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _repo;
        public StockService(IStockRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<Stock?> GetByIdsAsync(int playerId, int companyId)
        {
            return await _repo.GetByIdsAsync(playerId, companyId);
        }
        public async Task<List<Stock>> GetByPlayerIdAsync(int playerId)
        {
            return await _repo.GetByPlayerIdAsync(playerId);
        }

        public async Task<List<Stock>> GetByCompanyIdAsync(int companyId)
        {
            return await _repo.GetByCompanyIdAsync(companyId);
        }

        public async Task<bool> AddOrUpdateAsync(int playerId, int companyId, int sharesOwned)
        {
            var stock = await _repo.GetByIdsAsync(playerId, companyId);

            if (stock == null)
            {
                if (sharesOwned <= 0)
                    return false; // nothing to add

                stock = new Stock
                {
                    PlayerId = playerId,
                    CompanyId = companyId,
                    SharesOwned = sharesOwned
                };
                await _repo.AddAsync(stock);
            }
            else
            {
                stock.SharesOwned = sharesOwned;

                // if shares go to zero, delete instead of updating
                if (sharesOwned <= 0)
                {
                    await _repo.DeleteAsync(playerId, companyId);
                }
                else
                {
                    await _repo.UpdateAsync(stock);
                }
            }

            return true;
        }

        public async Task<bool> DeleteAsync(int playerId, int companyId)
        {
            var stock = await _repo.GetByIdsAsync(playerId, companyId);
            if (stock == null)
                return false;

            await _repo.DeleteAsync(playerId, companyId);
            return true;
        }
    }

}