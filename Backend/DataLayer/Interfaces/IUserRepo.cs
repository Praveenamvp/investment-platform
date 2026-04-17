using Models.Entity;
using Models.Request;

namespace DataLayer.Interfaces
{
    public interface IUserRepo
    {
        public Task<bool> Add(UserDetails user);
        public Task<UserDetails> Get(UserRequest userRequest);
        public Task<UserDetails> GetByUID(Guid uid);

    }
}
