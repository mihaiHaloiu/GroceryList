using GroceryList.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryList.DAL.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDB>> GetAllUsers();

        Task<UserDB> GetUserByID(string id);

        Task<UserDB> GetUserByEmail(string email);
    }
}
