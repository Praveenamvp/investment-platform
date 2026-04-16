using Models.Entity;
using System.Data;

namespace Mapper.Interfaces
{
    public interface IDataLayerMapper
    {
        public Task<UserDetails> FetchUserDetails(DataRow row);

    }
}
