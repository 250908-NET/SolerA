using Railways.Models;

namespace Railways.Services
{
    public interface IStockService
    {
        public Task<List<Stock>> GetAllASync();
        public Task<Stock?> GetByIdAsync(int id);
        public Task CreateAsync(Stock stock);
    }
