using Models.Entity;
using Models.Request;

namespace BusinessLayer.Interfaces
{
    public interface IStockService
    {
        List<Stock> GetAllStocks();
        void InsertTransaction(TransactionRequest req);
    }
}
