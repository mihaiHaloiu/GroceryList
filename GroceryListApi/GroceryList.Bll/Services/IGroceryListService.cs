using GroceryList.Bll.Models;
using GroceryList.Bll.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroceryList.Bll.Services
{
    public interface IGroceryListService
    {

        Task<GroceryListModel> GetGroceryListByID(string ID);

        Task<IEnumerable<GroceryListModel>> GetAllGroceryLists();

        Task AddGroceryList(GroceryListRequest groceryListRequest);

        Task GroceryListAddItem(string ID, string item);

        Task GroceryListRemovedItem(string ID, string item);

        Task DeleteGroceryList(string ID);
    }
}
