using GroceryList.Bll.Models;
using GroceryList.Bll.Models.Requests;
using GroceryList.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryList.Bll.Mappers
{
    public interface IGroceryListMapper
    {
        IEnumerable<GroceryListModel> GroceryListDBToGroceryListModel(IEnumerable<GroceryListDB> items);
        GroceryListModel GroceryListDBToGroceryListModel(GroceryListDB groceryListDB);

        GroceryListDB GroceryListRequestToGroceryListDB(GroceryListRequest groceryListModel);
    }
}
