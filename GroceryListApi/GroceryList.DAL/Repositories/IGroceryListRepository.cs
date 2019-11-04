using GroceryList.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroceryList.DAL.Repositories
{
    public interface IGroceryListRepository
    {
        Task<GroceryListDB> GetGroceryListByID(string ID);

        Task<IEnumerable<GroceryListDB>> GetAllGroceryLists();

        Task AddGroceryList(GroceryListDB groceryListDB);

        Task UpdateGroceryList(GroceryListDB groceryListDB);

        Task DeleteGroceryList(string ID);
    }
}
