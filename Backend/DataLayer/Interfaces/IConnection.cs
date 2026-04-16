using Microsoft.Data.SqlClient;

namespace DataLayer.Interfaces
{
    public interface IConnection
    {
        public SqlConnection GetConnection();

    }
}
