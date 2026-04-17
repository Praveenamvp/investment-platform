using DataLayer.Interfaces;
using Microsoft.Data.SqlClient;
using Models.Entity;
using System.Data;

namespace DataLayer.Implementations
{
    public class StockRepo : IStockRepo
    {
        private readonly IConnection _connection;

        public StockRepo(IConnection connection)
        {
            _connection = connection;
        }

        public List<Stock> GetAllStocks()
        {
            List<Stock> stocks = new List<Stock>();
            
            SqlDataAdapter adapter = new SqlDataAdapter("GetAllStocks", _connection.GetConnection());
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Stock stock = new Stock
                    {
                        StockId = Guid.Parse(row["StockId"].ToString()),
                        Symbol = row["Symbol"].ToString(),
                        Name = row["Name"].ToString(),
                        CurrentPrice = Convert.ToDecimal(row["CurrentPrice"]),
                        ChangePercent = Convert.ToDecimal(row["ChangePercent"])
                    };
                    stocks.Add(stock);
                }
            }
            
            return stocks;
        }
    }
}
