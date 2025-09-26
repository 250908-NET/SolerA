using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Railways.Models;
using Railways.Services;
using Railways.Repositories;
using Railways.Domain;

namespace Railways.Tests.Services
{
    public class CompanyServiceTests
    {
        private readonly Mock<ICompanyRepository> _companyRepoMock;
        private readonly Mock<IStockRepository> _stockRepoMock;
        private readonly Mock<IPlayerRepository> _playerRepoMock;
        private readonly CompanyService _service;

        public CompanyServiceTests()
        {
            _companyRepoMock = new Mock<ICompanyRepository>();
            _stockRepoMock = new Mock<IStockRepository>();
            _playerRepoMock = new Mock<IPlayerRepository>();

            _service = new CompanyService(
                _companyRepoMock.Object,
                _stockRepoMock.Object,
                _playerRepoMock.Object
            );
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCompanies()
        {
            var companies = new List<Company> { new Company { Id = 1, Name = "TestCo" } };
            _companyRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(companies);

            var result = await _service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal("TestCo", result[0].Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCompany()
        {
            var company = new Company { Id = 1, Name = "TestCo" };
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(company);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("TestCo", result!.Name);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateCompany()
        {
            var result = await _service.CreateAsync("NewCo", 1000, 90);

            Assert.Equal("NewCo", result.Name);
            Assert.Equal(1000, result.Money);
            _companyRepoMock.Verify(r => r.AddAsync(It.IsAny<Company>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldAlwaysReturnTrue()
        {
            var result = await _service.DeleteAsync(1);

            Assert.True(result);
            _companyRepoMock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task PayoutAsync_ShouldReturnFalse_WhenCompanyNotFound()
        {
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Company?)null);

            var result = await _service.PayoutAsync(1, 100);

            Assert.False(result);
        }

        [Fact]
        public async Task PayoutAsync_ShouldAdjustStockPriceAndPayPlayers()
        {
            var company = new Company { Id = 1, TotalShares = 5, StockPriceIndex = 1 };
            var player = new Player { Id = 1, Money = 100 };
            var stock = new Stock { PlayerId = 1, CompanyId = 1, SharesOwned = 2 };

            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(company);
            _stockRepoMock.Setup(r => r.GetByCompanyIdAsync(1)).ReturnsAsync(new List<Stock> { stock });
            _playerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);

            var result = await _service.PayoutAsync(1, StockMarket.Track[company.StockPriceIndex]);

            Assert.True(result);
            Assert.True(player.Money > 100); // player got paid
            _playerRepoMock.Verify(r => r.UpdateAsync(player), Times.Once);
            _companyRepoMock.Verify(r => r.UpdateAsync(company), Times.Once);
        }

        [Fact]
        public async Task PayoutAsync_ShouldPayTreasuryShares()
        {
            var company = new Company { Id = 1, TotalShares = 5, StockPriceIndex = 1, Money = 0 };
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(company);
            _stockRepoMock.Setup(r => r.GetByCompanyIdAsync(1)).ReturnsAsync(new List<Stock>());

            var result = await _service.PayoutAsync(1, 100);

            Assert.True(result);
            Assert.True(company.Money > 0);
            _companyRepoMock.Verify(r => r.UpdateAsync(company), Times.Once);
        }

        [Fact]
        public async Task WithholdAsync_ShouldReturnFalse_WhenCompanyNotFound()
        {
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Company?)null);

            var result = await _service.WithholdAsync(1, 200);

            Assert.False(result);
        }

        [Fact]
        public async Task WithholdAsync_ShouldIncreaseMoney_AndDecreaseStockPriceIndex()
        {
            var company = new Company { Id = 1, Money = 100, StockPriceIndex = 3 };
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(company);

            var result = await _service.WithholdAsync(1, 50);

            Assert.True(result);
            Assert.Equal(150, company.Money);
            _companyRepoMock.Verify(r => r.UpdateAsync(company), Times.Once);
        }

        [Fact]
        public async Task AdjustMoneyAsync_ShouldReturnFalse_WhenCompanyNotFound()
        {
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Company?)null);

            var result = await _service.AdjustMoneyAsync(1, 500);

            Assert.False(result);
        }

        [Fact]
        public async Task AdjustMoneyAsync_ShouldChangeMoney_WhenCompanyExists()
        {
            var company = new Company { Id = 1, Money = 100 };
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(company);

            var result = await _service.AdjustMoneyAsync(1, 200);

            Assert.True(result);
            Assert.Equal(300, company.Money);
            _companyRepoMock.Verify(r => r.UpdateAsync(company), Times.Once);
        }
    }
}
