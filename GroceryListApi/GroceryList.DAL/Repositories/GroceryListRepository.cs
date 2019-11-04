using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using GroceryList.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroceryList.DAL.Repositories
{
    public class GroceryListRepository : IGroceryListRepository
    {
        private readonly DynamoDBContext _context;

        public GroceryListRepository(IAmazonDynamoDB dynamoDBClient)
        {
            _context = new DynamoDBContext(dynamoDBClient);
        }

        public async Task<GroceryListDB> GetGroceryListByID(string ID)
        {
            GroceryListDB result = await _context.LoadAsync<GroceryListDB>(ID);

            return result;
        }

        public async Task<IEnumerable<GroceryListDB>> GetAllGroceryLists()
        {
            IEnumerable<GroceryListDB> result = await _context.ScanAsync<GroceryListDB>(new List<ScanCondition>()).GetRemainingAsync();

            return result;
        }

        public async Task AddGroceryList(GroceryListDB groceryListDB)
        {
            await _context.SaveAsync(groceryListDB);
        }
        public async Task UpdateGroceryList(GroceryListDB groceryListDB)
        {
            await _context.SaveAsync(groceryListDB);
        }

        public async Task DeleteGroceryList(string ID)
        {
            await _context.DeleteAsync<GroceryListDB>(ID);
        }
    }
}
