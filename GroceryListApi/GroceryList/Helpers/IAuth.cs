using GroceryList.Bll.Models;
using GroceryList.Bll.Models.Reponses;
using System.Threading.Tasks;

namespace GroceryList.Helpers
{
    public interface IAuth
    {
        Task<UserResponse> Authenticate(string email, string password);

        string GenerateHash(string input);

        byte[] GenerateSalt(int length);

        bool CheckPassword(string hash, string input);
    }
}
