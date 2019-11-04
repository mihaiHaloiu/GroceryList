using GroceryList.Bll.Mappers;
using GroceryList.Bll.Models;
using GroceryList.Bll.Models.Reponses;
using GroceryList.Bll.Services;
using GroceryList.DAL.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GroceryList.Helpers
{
    public class Auth : IAuth
    {
        private readonly AppSettings _appSettings;
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;

        public Auth(IOptions<AppSettings> appSettings, IUserService userService, IUserMapper userMapper)
        {
            _appSettings = appSettings.Value;
            _userService = userService;
            _userMapper = userMapper;
        }

        public async Task<UserResponse> Authenticate(string email, string password)
        {
            //var user = _users.SingleOrDefault(x => x.Email == username && x.Password == password);

            User user = await _userService.GetUserByEmail(email);

            // return null if user not found
            if (user == null || !CheckPassword(user.Password, password))
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {                               
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString()),
                    new Claim(ClaimTypes.GivenName, user.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);            

            UserResponse userResponse = _userMapper.UserToUserResponse(user);
            userResponse.Token = tokenHandler.WriteToken(token);            

            return userResponse;
        }

        public string GenerateHash(string input)
        {
            var salt = GenerateSalt(16);

            var bytes = KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

            return $"{ Convert.ToBase64String(salt) }:{ Convert.ToBase64String(bytes) }";
        }

        public byte[] GenerateSalt(int length)
        {
            var salt = new byte[length];

            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            return salt;
        }

        public bool CheckPassword(string hash, string input)
        {
            try
            {
                var parts = hash.Split(':');

                var salt = Convert.FromBase64String(parts[0]);

                var bytes = KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

                return parts[1].Equals(Convert.ToBase64String(bytes));
            }
            catch
            {
                return false;
            }
        }
    }
}
