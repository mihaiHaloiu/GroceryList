using GroceryList.Bll.Mappers;
using GroceryList.Bll.Models;
using GroceryList.Bll.Models.Requests;
using GroceryList.DAL.Models;
using GroceryList.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryList.Bll.Services
{
    public class GroceryListService : IGroceryListService
    {
        private IGroceryListRepository _groceryListRepository;
        private IGroceryListMapper _groceryListMapper;

        public GroceryListService(IGroceryListRepository groceryListRepository, IGroceryListMapper groceryListMapper)
        {
            _groceryListRepository = groceryListRepository;
            _groceryListMapper = groceryListMapper;
        }
        public async Task<GroceryListModel> GetGroceryListByID(string ID)
        {
            var response = await _groceryListRepository.GetGroceryListByID(ID);

            return _groceryListMapper.GroceryListDBToGroceryListModel(response);
        }

        public async Task<IEnumerable<GroceryListModel>> GetAllGroceryLists()
        {
            var response = await _groceryListRepository.GetAllGroceryLists();

            return _groceryListMapper.GroceryListDBToGroceryListModel(response);
        }

        public async Task AddGroceryList(GroceryListRequest groceryListRequest)
        {
            var groceryListDB = _groceryListMapper.GroceryListRequestToGroceryListDB(groceryListRequest);

            await _groceryListRepository.AddGroceryList(groceryListDB);
        }

        public async Task GroceryListAddItem(string ID, string item)
        {

            GroceryListDB groceryListDB = await _groceryListRepository.GetGroceryListByID(ID);

            if(groceryListDB.items == null)
            {
                groceryListDB.items = new List<string>();
            }
            groceryListDB.items.Add(item);

            await _groceryListRepository.UpdateGroceryList(groceryListDB);
        }

        public async Task GroceryListRemovedItem(string ID, string item)
        {
            GroceryListDB groceryListDB = await _groceryListRepository.GetGroceryListByID(ID);

            if (groceryListDB.items != null)
            {
                groceryListDB.items.Remove(item);

                await _groceryListRepository.UpdateGroceryList(groceryListDB);
            }
        }

        public async Task DeleteGroceryList(string ID)
        {
            await _groceryListRepository.DeleteGroceryList(ID);
        }

    }
}
