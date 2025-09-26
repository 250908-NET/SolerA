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
           return await _context.Companies.ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(int id) =>
            await _context.Companies
                .Include(c => c.Stocks)
                .ThenInclude(s => s.Player)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddAsync(Company company)
        {
            _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }
    }
}