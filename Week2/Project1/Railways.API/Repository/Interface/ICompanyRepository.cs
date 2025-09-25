using Railways.Models;

namespace Railways.Repositories
{
    public interface ICompanyRepository
    {
        public Task<List<Company>> GetAllAsync();
        public Task<Company?> GetByIdAsync(int id);
        public Task AddAsync(Company company);
        public Task SaveChangesAsync();
    }
}