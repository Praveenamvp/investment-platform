using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using Models.Entity;
using Models.Request;

namespace BusinessLayer.Implementations
{
    public class StockService : IStockService
    {
        private readonly IStockRepo _repo;
        private readonly IMutualFundRepo _fundRepo;

        public StockService(IStockRepo repo, IMutualFundRepo fundRepo)
        {
            _repo = repo;
            _fundRepo = fundRepo;
        }

        public List<Stock> GetAllStocks()
        {
            return _repo.GetAllStocks();
        }

        public void InsertTransaction(TransactionRequest req)
        {
            var portfolio = _repo.GetByUserId(req.UserId);

            if (portfolio == null)
            {
                portfolio = new Portfolio
                {
                    PortfolioId = Guid.NewGuid(),
                    UserId = req.UserId
                };

                _repo.CreatePortfolio(portfolio);
            }

            decimal price = 0;

            // 2. Get Price based on Type
            if (req.AssetType == "Stock")
            {
                var stock = _repo.GetById(req.AssetId);
                if (stock == null)
                    throw new Exception("Stock not found");

                price = stock.CurrentPrice;
            }
            else if (req.AssetType == "MutualFund")
            {
                var fund = _fundRepo.GetById(req.AssetId);
                if (fund == null)
                    throw new Exception("Fund not found");

                price = fund.NAV;
            }
            else
            {
                throw new Exception("Invalid asset type");
            }

            // 3. Insert Transaction
            _repo.InsertTransaction(
                portfolio.PortfolioId,
                req.AssetId,
                req.AssetType,
                req.Quantity,
                price
            );
        }
    }
}
