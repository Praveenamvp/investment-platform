using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using Models.DTOs;
using Models.Entity;
using Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly ITokenGenerate _tokenGenerate;

        public UserService(IUserRepo userRepo, ITokenGenerate tokenGenerate)
        {
            _userRepo = userRepo;
            _tokenGenerate = tokenGenerate;
        }
        public async Task<bool> Add(UserRegisterRequest userRegisterRequest)
        {
            UserDetails userdata = new UserDetails();
            userdata.UserName = userRegisterRequest.UserName;
            userdata.PhoneNumber = userRegisterRequest.PhoneNumber;

            userdata.Email = userRegisterRequest.Email;
            var hmac = new HMACSHA512();
            userdata.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userRegisterRequest.Password ?? ""));
            userdata.PasswordKey = hmac.Key;
            if (await _userRepo.Add(userdata))
            {
                return true;
            }
            return false;
        }

        public async Task<UserDetails> Get(Guid uid)
        {
            UserDetails userDetails = await _userRepo.GetByUID(uid);
            if (userDetails != null)
            {
                return userDetails;
            }
            return null;
        }

        public async Task<UserDTO> LoginUser(UserRequest userRequest)
        {
            if (userRequest == null || string.IsNullOrEmpty(userRequest.Password))
            {
                return null;
            }

            var userData = await _userRepo.Get(userRequest);
            // Validate that user exists and stored credentials are present
            if (userData == null || userData.PasswordKey == null || userData.PasswordHash == null)
            {
                return null;
            }

            // Use the stored key to recreate the HMAC and compute the hash of the provided password.
            using var hmac = new HMACSHA512(userData.PasswordKey);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userRequest.Password));

            // Use constant-time comparison to avoid timing attacks and handle length mismatch safely.
            if (!CryptographicOperations.FixedTimeEquals(computedHash, userData.PasswordHash))
            {
                return null;
            }

            var userDetails = new UserDTO
            {
                UID = userData.UID,
                Email = userData.Email,
                Name = userData.UserName,
                PhoneNumber = userData.PhoneNumber,
                Token = await _tokenGenerate.GenerateToken(userData.Email)
            };

            return userDetails;

        }
    }
}
