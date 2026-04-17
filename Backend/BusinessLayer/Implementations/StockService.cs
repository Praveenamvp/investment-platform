using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using Models.Entity;

namespace BusinessLayer.Implementations
{
    public class StockService : IStockService
    {
        private readonly IStockRepo _repo;

        public StockService(IStockRepo repo)
        {
            _repo = repo;
        }

        public List<Stock> GetAllStocks()
        {
            return _repo.GetAllStocks();
        }
    }
}
