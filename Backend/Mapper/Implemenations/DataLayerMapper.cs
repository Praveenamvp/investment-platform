using Mapper.Interfaces;
using Models.Entity;
using System.Data;

namespace Mapper.Implemenations
{
    public class DataLayerMapper : IDataLayerMapper
    {
        public async Task<UserDetails> FetchUserDetails(DataRow row)
        {
            UserDetails userDetails = new UserDetails();
            userDetails.UID = Guid.Parse(row[0].ToString());

            userDetails.UserName = row[1].ToString();
            userDetails.Email = row[2].ToString();
            userDetails.PhoneNumber = row[3].ToString();
            userDetails.PasswordKey = (byte[])row[4];
            userDetails.PasswordHash = (byte[])row[5];

            return userDetails;


        }
    }

}