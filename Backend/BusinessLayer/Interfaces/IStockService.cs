using Models.Entity;

namespace BusinessLayer.Interfaces
{
    public interface IStockService
    {
        List<Stock> GetAllStocks();
    }
}
