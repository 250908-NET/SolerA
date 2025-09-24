using Railways.Models;

namespace Railways.Services
{
    public interface ICompanyService
    {
        public Task<List<Company>> GetAllASync();
        public Task<Company?> GetByIdAsync(int id);
        public Task CreateAsync(Company company);
    }
