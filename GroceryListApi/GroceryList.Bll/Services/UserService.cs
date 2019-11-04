using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryList.Bll.Mappers;
using GroceryList.Bll.Models;
using GroceryList.DAL.Repositories;
using Microsoft.Extensions.Options;

namespace GroceryList.Bll.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserMapper _userMapper;

        private List<User> _users = new List<User>
        {
            new User { ID = "1", Name = "Test test", Email= "test", Password = "test" }
        };

        public UserService(IUserRepository userRepositor, IUserMapper userMapper)
        {
            _userRepository = userRepositor;
            _userMapper = userMapper;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var response = await _userRepository.GetAllUsers();

            return _userMapper.UserDBToMovieModel(response);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var response = await _userRepository.GetUserByEmail(email);

            //var response = await _userRepository.GetUserByID("1");

            return _userMapper.UserDBToMovieModel(response);
        }
    }
}
