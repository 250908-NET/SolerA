/*
*  Additional Tests for Coverage:
*
*  GetByIdAsync returns player.
*
*  BuySharesAsync fails when player not found.
*
*  BuySharesAsync fails when company not found.
*
*  SellSharesAsync fails when player not found.
*
*  SellSharesAsync fails when company not found.
*
*  SellSharesAsync fails when player doesn’t own enough shares.
*
*  IPOAsync fails when company not found.
*
*  IPOAsync fails when company already has a president.
*/

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
    public class PlayerServiceTests
    {
        private readonly Mock<IPlayerRepository> _playerRepoMock;
        private readonly Mock<ICompanyRepository> _companyRepoMock;
        private readonly Mock<IStockRepository> _stockRepoMock;
        private readonly PlayerService _service;

        public PlayerServiceTests()
        {
            _playerRepoMock = new Mock<IPlayerRepository>();
            _companyRepoMock = new Mock<ICompanyRepository>();
            _stockRepoMock = new Mock<IStockRepository>();

            _service = new PlayerService(
                _playerRepoMock.Object,
                _companyRepoMock.Object,
                _stockRepoMock.Object
            );
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnPlayer_AndCallAddAsync()
        {
            // Arrange
            var username = "Alice";
            var money = 1000;

            _playerRepoMock.Setup(r => r.AddAsync(It.IsAny<Player>()))
                .Returns(Task.CompletedTask);

            // Act
            var player = await _service.CreateAsync(username, money);

            // Assert
            Assert.Equal(username, player.Username);
            Assert.Equal(money, player.Money);
            _playerRepoMock.Verify(r => r.AddAsync(It.IsAny<Player>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnPlayers()
        {
            // Arrange
            var players = new List<Player>
            {
                new Player { Id = 1, Username = "Bob", Money = 500 }
            };

            _playerRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(players);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Bob", result[0].Username);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenPlayerDoesNotExist()
        {
            // Arrange
            _playerRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Player?)null);

            // Act
            var result = await _service.DeleteAsync(99);

            // Assert
            Assert.False(result);
            _playerRepoMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task BuySharesAsync_ShouldDeductMoneyAndAddStock_WhenBankHasShares()
        {
            // Arrange
            var player = new Player { Id = 1, Username = "Bob", Money = 1000 };
            var company = new Company { Id = 1, TotalShares = 10, StockPriceIndex = 1 };
            var bankStock = new Stock { PlayerId = -1, CompanyId = 1, SharesOwned = 5 };

            _playerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(company);
            _stockRepoMock.Setup(r => r.GetByIdsAsync(-1, 1)).ReturnsAsync(bankStock);
            _stockRepoMock.Setup(r => r.GetByIdsAsync(1, 1)).ReturnsAsync((Stock?)null);

            // Act
            var result = await _service.BuySharesAsync(1, 1, 2);

            // Assert
            Assert.True(result);
            Assert.Equal(1000 - (StockMarket.Track[1] * 2), player.Money);
            Assert.Equal(3, bankStock.SharesOwned);
            _stockRepoMock.Verify(r => r.UpdateAsync(bankStock), Times.Once);
            _playerRepoMock.Verify(r => r.UpdateAsync(player), Times.Once);
        }

        [Fact]
        public async Task BuySharesAsync_ShouldFail_WhenOver60PercentOwnership()
        {
            // Arrange
            var player = new Player { Id = 1, Money = 5000 };
            var company = new Company { Id = 1, TotalShares = 10, StockPriceIndex = 1 };
            var playerStock = new Stock { PlayerId = 1, CompanyId = 1, SharesOwned = 6 };

            _playerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(company);
            _stockRepoMock.Setup(r => r.GetByIdsAsync(1, 1)).ReturnsAsync(playerStock);

            // Act
            var result = await _service.BuySharesAsync(1, 1, 1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task SellSharesAsync_ShouldIncreaseMoneyAndUpdateBankStock()
        {
            // Arrange
            var player = new Player { Id = 1, Money = 1000 };
            var company = new Company { Id = 1, StockPriceIndex = 1 };
            var playerStock = new Stock { PlayerId = 1, CompanyId = 1, SharesOwned = 3 };
            var bankStock = new Stock { PlayerId = -1, CompanyId = 1, SharesOwned = 0 };

            _playerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(company);
            _stockRepoMock.Setup(r => r.GetByIdsAsync(1, 1)).ReturnsAsync(playerStock);
            _stockRepoMock.Setup(r => r.GetByIdsAsync(-1, 1)).ReturnsAsync(bankStock);

            // Act
            var result = await _service.SellSharesAsync(1, 1, 2);

            // Assert
            Assert.True(result);
            Assert.Equal(1, playerStock.SharesOwned);
            Assert.True(player.Money > 1000); // money increased
            Assert.Equal(2, bankStock.SharesOwned);
            _playerRepoMock.Verify(r => r.UpdateAsync(player), Times.Once);
            _stockRepoMock.Verify(r => r.UpdateAsync(playerStock), Times.Once);
        }

        [Fact]
        public async Task IPOAsync_ShouldAssignPresidentAndGiveShares()
        {
            // Arrange
            var player = new Player { Id = 1, Money = 1000 };
            var company = new Company { Id = 1, StockPriceIndex = 0, PresidentId = null };

            _playerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(company);
            _stockRepoMock.Setup(r => r.GetByIdsAsync(1, 1)).ReturnsAsync((Stock?)null);

            // Act
            var result = await _service.IPOAsync(1, 1, 100);

            // Assert
            Assert.True(result);
            Assert.Equal(1, company.PresidentId);
            _companyRepoMock.Verify(r => r.UpdateAsync(company), Times.Once);
            _stockRepoMock.Verify(r => r.AddAsync(It.Is<Stock>(s => s.SharesOwned == 2)), Times.Once);
        }

        [Fact] // Type: Unit test (Delete success path)
        public async Task DeleteAsync_ShouldDeletePlayer_WhenExists()
        {
            var player = new Player { Id = 5, Username = "X", Money = 100 };
            _playerRepoMock.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(player);

            var result = await _service.DeleteAsync(5);

            Assert.True(result);
            _playerRepoMock.Verify(r => r.DeleteAsync(5), Times.Once);
        }

        [Fact] // Type: Negative path (Buy fails with no money)
        public async Task BuySharesAsync_ShouldFail_WhenPlayerHasInsufficientFunds()
        {
            var player = new Player { Id = 1, Money = 1 };
            var company = new Company { Id = 1, TotalShares = 10, StockPriceIndex = 2 };
            var bankStock = new Stock { PlayerId = -1, CompanyId = 1, SharesOwned = 10 };

            _playerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(company);
            _stockRepoMock.Setup(r => r.GetByIdsAsync(-1, 1)).ReturnsAsync(bankStock);

            var result = await _service.BuySharesAsync(1, 1, 1);

            Assert.False(result);
        }

        [Fact] // Type: Negative path (Sell fails if player has no stock)
        public async Task SellSharesAsync_ShouldFail_WhenPlayerHasNoStock()
        {
            var player = new Player { Id = 1, Money = 100 };
            var company = new Company { Id = 1, StockPriceIndex = 1 };

            _playerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(company);
            _stockRepoMock.Setup(r => r.GetByIdsAsync(1, 1)).ReturnsAsync((Stock?)null);

            var result = await _service.SellSharesAsync(1, 1, 1);

            Assert.False(result);
        }

        [Fact] // Type: Negative path (IPO fails if already IPO’d)
        public async Task IPOAsync_ShouldFail_WhenCompanyAlreadyHasPresident()
        {
            var player = new Player { Id = 1, Money = 1000 };
            var company = new Company { Id = 1, PresidentId = 2 }; // already IPO’d

            _playerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(company);

            var result = await _service.IPOAsync(1, 1, 100);

            Assert.False(result);
        }

        [Fact] // Type: Integration logic (Merge companies)
        public async Task MergeCompaniesAsync_ShouldCombineTwoMinorsIntoMajor()
        {
            var player = new Player { Id = 1 };
            var company1 = new Company { Id = 10, Money = 100, StockPriceIndex = 2 };
            var company2 = new Company { Id = 11, Money = 200, StockPriceIndex = 4 };
            var stock1 = new Stock { PlayerId = 1, CompanyId = 10, SharesOwned = 3 };
            var stock2 = new Stock { PlayerId = 2, CompanyId = 11, SharesOwned = 2 };

            _playerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);
            _companyRepoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(company1);
            _companyRepoMock.Setup(r => r.GetByIdAsync(11)).ReturnsAsync(company2);
            _stockRepoMock.Setup(r => r.GetByCompanyIdAsync(10)).ReturnsAsync(new List<Stock> { stock1 });
            _stockRepoMock.Setup(r => r.GetByCompanyIdAsync(11)).ReturnsAsync(new List<Stock> { stock2 });

            Company? merged = await _service.MergeCompaniesAsync(1, 10, 11, "BigCorp");

            Assert.NotNull(merged);
            Assert.Equal("BigCorp", merged!.Name);
            Assert.Equal(1, merged.PresidentId);
            _companyRepoMock.Verify(r => r.AddAsync(It.IsAny<Company>()), Times.Once);
            _companyRepoMock.Verify(r => r.DeleteAsync(10), Times.Once);
            _companyRepoMock.Verify(r => r.DeleteAsync(11), Times.Once);
            _stockRepoMock.Verify(r => r.DeleteAsync(1, 10), Times.Once);
            _stockRepoMock.Verify(r => r.DeleteAsync(2, 11), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnPlayer_WhenExists()
        {
            // Arrange
            var player = new Player { Id = 42, Username = "Test", Money = 100 };
            _playerRepoMock.Setup(r => r.GetByIdAsync(42)).ReturnsAsync(player);

            // Act
            var result = await _service.GetByIdAsync(42);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.Username);
        }

        /* Negative test: BuySharesAsync fails when player not found */
        [Fact]
        public async Task BuySharesAsync_ShouldFail_WhenPlayerNotFound()
        {
            _playerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Player?)null);

            var result = await _service.BuySharesAsync(1, 1, 1);

            Assert.False(result);
        }

        /* Negative test: BuySharesAsync fails when company not found */
        [Fact]
        public async Task BuySharesAsync_ShouldFail_WhenCompanyNotFound()
        {
            var player = new Player { Id = 1, Money = 1000 };
            _playerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Company?)null);

            var result = await _service.BuySharesAsync(1, 1, 1);

            Assert.False(result);
        }

        /* Negative test: SellSharesAsync fails when player not found */
        [Fact]
        public async Task SellSharesAsync_ShouldFail_WhenPlayerNotFound()
        {
            _playerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Player?)null);

            var result = await _service.SellSharesAsync(1, 1, 1);

            Assert.False(result);
        }

        /* Negative test: SellSharesAsync fails when company not found */
        [Fact]
        public async Task SellSharesAsync_ShouldFail_WhenCompanyNotFound()
        {
            var player = new Player { Id = 1, Money = 1000 };
            _playerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Company?)null);

            var result = await _service.SellSharesAsync(1, 1, 1);

            Assert.False(result);
        }

        /* Negative test: SellSharesAsync fails when not enough shares */
        [Fact]
        public async Task SellSharesAsync_ShouldFail_WhenNotEnoughShares()
        {
            var player = new Player { Id = 1, Money = 1000 };
            var company = new Company { Id = 1, StockPriceIndex = 1 };
            var playerStock = new Stock { PlayerId = 1, CompanyId = 1, SharesOwned = 1 };

            _playerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(company);
            _stockRepoMock.Setup(r => r.GetByIdsAsync(1, 1)).ReturnsAsync(playerStock);

            var result = await _service.SellSharesAsync(1, 1, 2);

            Assert.False(result);
        }


        /* Negative test: IPOAsync fails when company not found */
        [Fact]
        public async Task IPOAsync_ShouldFail_WhenCompanyNotFound()
        {
            var player = new Player { Id = 1 };
            _playerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);
            _companyRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Company?)null);

            var result = await _service.IPOAsync(1, 1, 100);

            Assert.False(result);
        }
    }
}
