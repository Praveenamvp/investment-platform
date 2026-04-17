using Models.Entity;

namespace DataLayer.Interfaces
{
    public interface IStockRepo
    {
        List<Stock> GetAllStocks();
    }
}
