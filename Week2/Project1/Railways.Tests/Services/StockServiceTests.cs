using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Railways.Models;
using Railways.Services;
using Railways.Repositories;

namespace Railways.Tests.Services
{
    public class StockServiceTests
    {
        private readonly Mock<IStockRepository> _repoMock;
        private readonly StockService _service;

        public StockServiceTests()
        {
            _repoMock = new Mock<IStockRepository>();
            _service = new StockService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnStocks()
        {
            var stocks = new List<Stock> { new Stock { PlayerId = 1, CompanyId = 1, SharesOwned = 5 } };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(stocks);

            var result = await _service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal(5, result[0].SharesOwned);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoStocksExist()
        {
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Stock>());

            var result = await _service.GetAllAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByIdsAsync_ShouldReturnStock()
        {
            var stock = new Stock { PlayerId = 1, CompanyId = 2, SharesOwned = 10 };
            _repoMock.Setup(r => r.GetByIdsAsync(1, 2)).ReturnsAsync(stock);

            var result = await _service.GetByIdsAsync(1, 2);

            Assert.Equal(10, result.SharesOwned);
        }

        [Fact]
        public async Task GetByIdsAsync_ShouldReturnNull_WhenStockDoesNotExist()
        {
            _repoMock.Setup(r => r.GetByIdsAsync(1, 1)).ReturnsAsync((Stock?)null);

            var result = await _service.GetByIdsAsync(1, 1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByPlayerIdAsync_ShouldReturnStocks()
        {
            var stocks = new List<Stock> { new Stock { PlayerId = 1, CompanyId = 1, SharesOwned = 5 } };
            _repoMock.Setup(r => r.GetByPlayerIdAsync(1)).ReturnsAsync(stocks);

            var result = await _service.GetByPlayerIdAsync(1);

            Assert.Single(result);
            Assert.Equal(1, result[0].PlayerId);
        }

        [Fact]
        public async Task GetByPlayerIdAsync_ShouldReturnEmptyList_WhenNoStocksForPlayer()
        {
            _repoMock.Setup(r => r.GetByPlayerIdAsync(99)).ReturnsAsync(new List<Stock>());

            var result = await _service.GetByPlayerIdAsync(99);

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByCompanyIdAsync_ShouldReturnStocks()
        {
            var stocks = new List<Stock> { new Stock { PlayerId = 2, CompanyId = 1, SharesOwned = 8 } };
            _repoMock.Setup(r => r.GetByCompanyIdAsync(1)).ReturnsAsync(stocks);

            var result = await _service.GetByCompanyIdAsync(1);

            Assert.Single(result);
            Assert.Equal(1, result[0].CompanyId);
        }

        [Fact]
        public async Task GetByCompanyIdAsync_ShouldReturnEmptyList_WhenNoStocksForCompany()
        {
            _repoMock.Setup(r => r.GetByCompanyIdAsync(99)).ReturnsAsync(new List<Stock>());

            var result = await _service.GetByCompanyIdAsync(99);

            Assert.Empty(result);
        }

        [Fact]
        public async Task AddOrUpdateAsync_ShouldAdd_WhenStockNotFound_AndSharesPositive()
        {
            _repoMock.Setup(r => r.GetByIdsAsync(1, 1)).ReturnsAsync((Stock?)null);

            var result = await _service.AddOrUpdateAsync(1, 1, 5);

            Assert.True(result);
            _repoMock.Verify(r => r.AddAsync(It.Is<Stock>(s => s.SharesOwned == 5)), Times.Once);
        }

        [Fact]
        public async Task AddOrUpdateAsync_ShouldReturnFalse_WhenStockNotFound_AndSharesZeroOrLess()
        {
            _repoMock.Setup(r => r.GetByIdsAsync(1, 1)).ReturnsAsync((Stock?)null);

            var result = await _service.AddOrUpdateAsync(1, 1, 0);

            Assert.False(result);
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Stock>()), Times.Never);
        }

        [Fact]
        public async Task AddOrUpdateAsync_ShouldUpdate_WhenStockExists_AndSharesPositive()
        {
            var stock = new Stock { PlayerId = 1, CompanyId = 1, SharesOwned = 2 };
            _repoMock.Setup(r => r.GetByIdsAsync(1, 1)).ReturnsAsync(stock);

            var result = await _service.AddOrUpdateAsync(1, 1, 10);

            Assert.True(result);
            Assert.Equal(10, stock.SharesOwned);
            _repoMock.Verify(r => r.UpdateAsync(stock), Times.Once);
        }

        [Fact]
        public async Task AddOrUpdateAsync_ShouldUpdate_WhenStockExists_AndSharesUnchanged()
        {
            var stock = new Stock { PlayerId = 1, CompanyId = 1, SharesOwned = 5 };
            _repoMock.Setup(r => r.GetByIdsAsync(1, 1)).ReturnsAsync(stock);

            var result = await _service.AddOrUpdateAsync(1, 1, 5);

            Assert.True(result);
            _repoMock.Verify(r => r.UpdateAsync(stock), Times.Once);
        }

        [Fact]
        public async Task AddOrUpdateAsync_ShouldDelete_WhenStockExists_AndSharesZeroOrLess()
        {
            var stock = new Stock { PlayerId = 1, CompanyId = 1, SharesOwned = 5 };
            _repoMock.Setup(r => r.GetByIdsAsync(1, 1)).ReturnsAsync(stock);

            var result = await _service.AddOrUpdateAsync(1, 1, 0);

            Assert.True(result);
            _repoMock.Verify(r => r.DeleteAsync(1, 1), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenStockNotFound()
        {
            _repoMock.Setup(r => r.GetByIdsAsync(1, 1)).ReturnsAsync((Stock?)null);

            var result = await _service.DeleteAsync(1, 1);

            Assert.False(result);
            _repoMock.Verify(r => r.DeleteAsync(1, 1), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDelete_WhenStockExists()
        {
            var stock = new Stock { PlayerId = 1, CompanyId = 1, SharesOwned = 3 };
            _repoMock.Setup(r => r.GetByIdsAsync(1, 1)).ReturnsAsync(stock);

            var result = await _service.DeleteAsync(1, 1);

            Assert.True(result);
            _repoMock.Verify(r => r.DeleteAsync(1, 1), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldNotThrow_WhenStockAlreadyDeleted()
        {
            _repoMock.Setup(r => r.GetByIdsAsync(1, 1)).ReturnsAsync((Stock?)null);

            var result = await _service.DeleteAsync(1, 1);

            Assert.False(result);
            _repoMock.Verify(r => r.DeleteAsync(1, 1), Times.Never);
        }
    }
}
