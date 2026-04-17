using DataLayer.Interfaces;
using Microsoft.Data.SqlClient;
using Models.Entity;
using System.Data;

namespace DataLayer.Implementations
{
    public class MutualFundRepo : IMutualFundRepo
    {
        private readonly IConnection _connection;

        public MutualFundRepo(IConnection connection)
        {
            _connection = connection;
        }

        public List<MutualFund> GetAllMutualFunds()
        {
            List<MutualFund> mutualFunds = new List<MutualFund>();

            SqlDataAdapter adapter = new SqlDataAdapter("GetAllMutualFunds", _connection.GetConnection());
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    MutualFund mutualFund = new MutualFund
                    {
                        MutualFundId = Guid.Parse(row["MutualFundId"].ToString()),
                        Name = row["Name"].ToString(),
                        NAV = Convert.ToDecimal(row["NAV"]),
                        ReturnsPercent = Convert.ToDecimal(row["ReturnsPercent"]),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                    };
                    mutualFunds.Add(mutualFund);
                }
            }

            return mutualFunds;
        }
    }
}
