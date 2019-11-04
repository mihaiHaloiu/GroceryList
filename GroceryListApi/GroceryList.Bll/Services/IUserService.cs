using GroceryList.Bll.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroceryList.Bll.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserByEmail(string email);
    }
}
