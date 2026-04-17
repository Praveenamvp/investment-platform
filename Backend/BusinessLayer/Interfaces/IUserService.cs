using Models.DTOs;
using Models.Entity;
using Models.Request;

namespace BusinessLayer.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> LoginUser(UserRequest userRequest);
        public Task<bool> Add(UserRegisterRequest userRegisterRequest);
        public Task<UserDetails> Get(Guid uid);

    }
}
