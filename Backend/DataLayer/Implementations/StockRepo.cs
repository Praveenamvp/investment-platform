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

        public Stock? GetById(Guid stockId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [data].[Stocks] WHERE StockId = @StockId", _connection.GetConnection());
            adapter.SelectCommand.Parameters.AddWithValue("@StockId", stockId);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataSet.Tables[0].Rows[0];
                return new Stock
                {
                    StockId = Guid.Parse(row["StockId"].ToString()),
                    Symbol = row["Symbol"].ToString(),
                    Name = row["Name"].ToString(),
                    CurrentPrice = Convert.ToDecimal(row["CurrentPrice"]),
                    ChangePercent = Convert.ToDecimal(row["ChangePercent"])
                };
            }
            return null;
        }

        public Portfolio? GetByUserId(Guid userId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [data].[Portfolio] WHERE UserId = @UserId", _connection.GetConnection());
            adapter.SelectCommand.Parameters.AddWithValue("@UserId", userId);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataSet.Tables[0].Rows[0];
                return new Portfolio
                {
                    PortfolioId = Guid.Parse(row["PortfolioId"].ToString()),
                    UserId = Guid.Parse(row["UserId"].ToString()),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                };
            }
            return null;
        }

        public void CreatePortfolio(Portfolio portfolio)
        {
            using SqlConnection conn = _connection.GetConnection();
            using SqlCommand cmd = new SqlCommand("INSERT INTO [data].[Portfolio] (PortfolioId, UserId) VALUES (@PortfolioId, @UserId)", conn);
            cmd.Parameters.AddWithValue("@PortfolioId", portfolio.PortfolioId);
            cmd.Parameters.AddWithValue("@UserId", portfolio.UserId);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void InsertTransaction(Guid portfolioId, Guid assetId, string type, decimal qty, decimal price)
        {
            using SqlConnection conn = _connection.GetConnection();
            using SqlCommand cmd = new SqlCommand("InsertTransaction", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PortfolioId", portfolioId);
            cmd.Parameters.AddWithValue("@AssetId", assetId);
            cmd.Parameters.AddWithValue("@AssetType", type);
            cmd.Parameters.AddWithValue("@Quantity", qty);
            cmd.Parameters.AddWithValue("@Price", price);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
