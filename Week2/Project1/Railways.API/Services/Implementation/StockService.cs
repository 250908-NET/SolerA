using Railways.Models;

namespace Railways.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepoository _repo;
        public StockService()
        {
            _repo = _repo;
        }

        public async Task<List<Stock>> GetAllASync()
        {
            await _repo.GetAllAsync();
        }
        public async Task<Stock?> GetByIdAsync(int id)
        {
            await _repo.GetByIdAsync(id);
        }
        public async Task CreateAsync(Stock stock)
        {
            await _repo.AddAsync(stock);
            await _repo.SaveChangesAsync();
        }
    }

}