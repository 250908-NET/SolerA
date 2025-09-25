using Railways.Models;
using Railways.Data;
using Microsoft.EntityFrameworkCore;

namespace Railways.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly RailwaysDbContext _context;

        public CompanyRepository(RailwaysDbContext context)
        {
            _context = context;
        }

        public async Task<List<Company>> GetAllAsync()
        {
           return await _context.Companys.ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            //return await _context.Companys.Where( Company => Company.id  == id);
            throw new NotImplementedException();
        }

        public async Task AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}