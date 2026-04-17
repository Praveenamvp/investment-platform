using Models.Entity;

namespace DataLayer.Interfaces
{
    public interface IStockRepo
    {
        List<Stock> GetAllStocks();
        Stock? GetById(Guid stockId);
        Portfolio? GetByUserId(Guid userId);
        void CreatePortfolio(Portfolio portfolio);
        void InsertTransaction(Guid portfolioId, Guid assetId, string type, decimal qty, decimal price);
    }
}
