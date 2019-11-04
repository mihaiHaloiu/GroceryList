using GroceryList.DAL.Models;
using System.Collections.Generic;
using GroceryList.Bll.Models;
using System.Linq;
using GroceryList.Bll.Models.Requests;
using System;

namespace GroceryList.Bll.Mappers
{
    public class GroceryListMapper : IGroceryListMapper
    {
        public IEnumerable<GroceryListModel> GroceryListDBToGroceryListModel(IEnumerable<GroceryListDB> items)
        {
            return items.Select(GroceryListDBToGroceryListModel);
        }

        public GroceryListModel GroceryListDBToGroceryListModel(GroceryListDB groceryListDB)
        {
            if (groceryListDB == null)
            {
                return null;
            }

            return new GroceryListModel
            {
                ID = groceryListDB.id,
                Title = groceryListDB.title,
                Description = groceryListDB.description,
                Items = groceryListDB.items
            };
        }

        public GroceryListDB GroceryListRequestToGroceryListDB(GroceryListRequest groceryListRequest)
        {
           
            return new GroceryListDB
            {
                id = Guid.NewGuid().ToString(),
                title = groceryListRequest.Title,
                description = groceryListRequest.Description,
                items = groceryListRequest.Items
            };
        }
    }
}
