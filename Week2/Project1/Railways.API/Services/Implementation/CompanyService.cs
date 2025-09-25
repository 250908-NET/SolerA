using Railways.Models;

namespace Railways.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepoository _repo;
        public CompanyService()
        {
            _repo = _repo;
        }

        public async Task<List<Company>> GetAllASync()
        {
            await _repo.GetAllAsync();
        }
        public async Task<Company?> GetByIdAsync(int id)
        {
            await _repo.GetByIdAsync(id);
        }
        public async Task CreateAsync(Company company)
        {
            await _repo.AddAsync(company);
            await _repo.SaveChangesAsync();
        }
    }

}