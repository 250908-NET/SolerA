using Railways.Models;
using Railways.Repositories;
using Railways.Domain;

namespace Railways.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepo;
        private readonly ICompanyRepository _companyRepo;
        private readonly IStockRepository _stockRepo;

        public PlayerService(
            IPlayerRepository playerRepo,
            ICompanyRepository companyRepo,
            IStockRepository stockRepo)
        {
            _playerRepo = playerRepo;
            _companyRepo = companyRepo;
            _stockRepo = stockRepo;
        }

        // ----------------- Basic CRUD -----------------

        public async Task<List<Player>> GetAllAsync()
        {
            return await _playerRepo.GetAllAsync();
        }

        public async Task<Player?> GetByIdAsync(int id)
        {
            return await _playerRepo.GetByIdAsync(id);
        }

        public async Task<Player> CreateAsync(string username, int startingMoney)
        {
            var player = new Player
            {
                Username = username,
                Money = startingMoney
            };

            await _playerRepo.AddAsync(player);
            return player;
        }

        public async Task<bool> DeleteAsync(int playerId)
        {
            var player = await _playerRepo.GetByIdAsync(playerId);
            if (player == null) return false;

            await _playerRepo.DeleteAsync(playerId);

            return true;
        }

        // ----------------- Game Actions -----------------

        // Buy shares (bank first, then treasury)
        public async Task<bool> BuySharesAsync(int playerId, int companyId, int shares)
        {
            var player = await _playerRepo.GetByIdAsync(playerId);
            var company = await _companyRepo.GetByIdAsync(companyId);
            var bankStock = await _stockRepo.GetByIdsAsync(-1, companyId); // bank

            if (player == null || company == null) return false;

            int pricePerShare = StockMarket.Track[company.StockPriceIndex];
            int totalCost = pricePerShare * shares;

            if (player.Money < totalCost) return false;

            // Check ownership cap: no more than 60% of total shares
            var playerStock = await _stockRepo.GetByIdsAsync(player.Id, company.Id);
            int alreadyOwned = playerStock?.SharesOwned ?? 0;
            if (alreadyOwned + shares > company.TotalShares * 0.6)
            {
                return false; // would exceed ownership cap
            }

            // Prefer buying from bank first
            if (bankStock != null && bankStock.SharesOwned >= shares)
            {
                bankStock.SharesOwned -= shares;
                await _stockRepo.UpdateAsync(bankStock);
            }
            else if (company.TreasuryShares >= shares)
            {
                // Buy from treasury
                company.Money += totalCost;
            }
            else
            {
                return false; // not enough shares available
            }

            // Deduct money from player
            player.Money -= totalCost;

            // Add stock to player
            if (playerStock == null)
            {
                playerStock = new Stock
                {
                    PlayerId = player.Id,
                    CompanyId = company.Id,
                    SharesOwned = shares
                };
                await _stockRepo.AddAsync(playerStock);
            }
            else
            {
                playerStock.SharesOwned += shares;
                await _stockRepo.UpdateAsync(playerStock);
            }

            await _companyRepo.UpdateAsync(company);
            await _playerRepo.UpdateAsync(player);

            return true;
        }


        // Sell shares (always to bank)
        public async Task<bool> SellSharesAsync(int playerId, int companyId, int shares)
        {
            var player = await _playerRepo.GetByIdAsync(playerId);
            var company = await _companyRepo.GetByIdAsync(companyId);
            var bankStock = await _stockRepo.GetByIdsAsync(-1, companyId);

            if (player == null || company == null) return false;

            var playerStock = await _stockRepo.GetByIdsAsync(player.Id, company.Id);
            if (playerStock == null || playerStock.SharesOwned < shares) return false;

            int pricePerShare = StockMarket.Track[company.StockPriceIndex];
            int totalValue = pricePerShare * shares;

            // Deduct from player
            playerStock.SharesOwned -= shares;

            // Give to bank
            if (bankStock == null)
            {
                bankStock = new Stock
                {
                    PlayerId = -1,
                    CompanyId = company.Id,
                    SharesOwned = shares
                };
                var existing = await _stockRepo.GetByIdsAsync(-1, company.Id);
                if (existing == null)
                {
                    await _stockRepo.AddAsync(bankStock);
                }
                else
                {
                    existing.SharesOwned = bankStock.SharesOwned;
                    await _stockRepo.UpdateAsync(existing);
                }

            }
            else
            {
                bankStock.SharesOwned += shares;
            }

            player.Money += totalValue;

            // Stock price goes down once per share sold
            company.StockPriceIndex = StockMarket.MoveDown(company.StockPriceIndex, shares);
            await _companyRepo.UpdateAsync(company);

            await _playerRepo.UpdateAsync(player);
            await _stockRepo.UpdateAsync(playerStock);

            return true;
        }

        // IPO: assign president, set stock, grant 2 president shares
        public async Task<bool> IPOAsync(int playerId, int companyId, int initialStockPrice)
        {
            var player = await _playerRepo.GetByIdAsync(playerId);
            var company = await _companyRepo.GetByIdAsync(companyId);

            if (player.Money < initialStockPrice)
                return false;

            if (player == null || company == null) return false;
            if (company.PresidentId != null) return false; // already IPO’d

            // Set initial stock price (use /2 here if that's your game rule)
            company.StockPriceIndex = StockMarket.GetClosestIndex(initialStockPrice);

            // Assign president
            company.PresidentId = player.Id;

            // Give president’s 2-share certificate
            var playerStock = await _stockRepo.GetByIdsAsync(player.Id, company.Id);
            if (playerStock == null)
            {
                playerStock = new Stock
                {
                    PlayerId = player.Id,
                    CompanyId = company.Id,
                    SharesOwned = 2
                };
                player.Money -= initialStockPrice;
                company.Money += initialStockPrice;

                await _stockRepo.AddAsync(playerStock);
            }

            
            

            // Save updates
            await _companyRepo.UpdateAsync(company);
            await _playerRepo.UpdateAsync(player);

            return true;
        }


        // Merge two minors into one major
        public async Task<Company?> MergeCompaniesAsync(int playerId, int companyId1, int companyId2, string majorCompanyName)
        {
            var player = await _playerRepo.GetByIdAsync(playerId);
            var company1 = await _companyRepo.GetByIdAsync(companyId1);
            var company2 = await _companyRepo.GetByIdAsync(companyId2);

            if (player == null || company1 == null || company2 == null) return null;

            var mergedCompany = new Company
            {
                Name = majorCompanyName,
                Money = company1.Money + company2.Money,
                StockPriceIndex = (company1.StockPriceIndex + company2.StockPriceIndex) / 2,
                PresidentId = player.Id
            };

            await _companyRepo.AddAsync(mergedCompany);

            // Move all shareholders
            var oldStocks = new List<Stock>();
            oldStocks.AddRange(await _stockRepo.GetByCompanyIdAsync(companyId1));
            oldStocks.AddRange(await _stockRepo.GetByCompanyIdAsync(companyId2));

            foreach (var stock in oldStocks)
            {
                var newStock = await _stockRepo.GetByIdsAsync(stock.PlayerId, mergedCompany.Id);
                if (newStock == null)
                {
                    newStock = new Stock
                    {
                        PlayerId = stock.PlayerId,
                        CompanyId = mergedCompany.Id,
                        SharesOwned = stock.SharesOwned
                    };
                    await _stockRepo.AddAsync(newStock);
                }
                else
                {
                    newStock.SharesOwned += stock.SharesOwned;
                }

                await _stockRepo.DeleteAsync(stock.PlayerId, stock.CompanyId);
            }

            // Retire minors
            await _companyRepo.DeleteAsync(company1.Id);
            await _companyRepo.DeleteAsync(company2.Id);

            return mergedCompany;
        }
    }
}
