using DataLayer.Interfaces;
using Mapper.Interfaces;
using Microsoft.Data.SqlClient;
using Models.Entity;
using Models.Request;
using System.Data;

namespace DataLayer.Implementations
{
    public class UserRepo : IUserRepo
    {
        private readonly IConnection _connection;
        private readonly ITokenGenerate _tokenGenerate;
        private readonly IDataLayerMapper _dataLayerMapper;
        SqlConnection conn;

        public UserRepo(IConnection connection, ITokenGenerate tokenGenerate, IDataLayerMapper dataLayerMapper)
        {
            _connection = connection;
            _tokenGenerate = tokenGenerate;
            _dataLayerMapper = dataLayerMapper;
            conn = new SqlConnection(@"Data Source=KANINI-LTP-625\SQLEXPRESS;Integrated Security=true;Initial Catalog=InvestmentApp;TrustServerCertificate=true");
        }

        public async Task<bool> Add(UserDetails user)
        {
            SqlCommand command = null;
            command = new SqlCommand("AddUserDetails", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserName", user.UserName);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
            command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            command.Parameters.AddWithValue("@PasswordKey", user.PasswordKey);

            conn.Open();
            var result = command.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public async Task<UserDetails> Get(UserRequest userRequest)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("FetchUserDetail", _connection.GetConnection());
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = userRequest.Email;
            DataSet set = new DataSet();
            adapter.Fill(set);
            DataRow row = set.Tables[0].Rows[0];
            return await _dataLayerMapper.FetchUserDetails(row);
        }

        public async Task<UserDetails> GetByUID(Guid uid)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("FetchUserDetailByUID", _connection.GetConnection());
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@uid", SqlDbType.UniqueIdentifier).Value = uid;
            DataSet set = new DataSet();
            adapter.Fill(set);
            DataRow row = set.Tables[0].Rows[0];
            return await _dataLayerMapper.FetchUserDetails(row);
        }
    }
}
