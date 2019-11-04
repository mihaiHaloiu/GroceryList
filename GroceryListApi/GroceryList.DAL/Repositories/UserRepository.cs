using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using GroceryList.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryList.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly DynamoDBContext _context;

        public UserRepository(IAmazonDynamoDB dynamoDBClient)
        {
            _context = new DynamoDBContext(dynamoDBClient);
        }

        public async Task<IEnumerable<UserDB>> GetAllUsers()
        {
            IEnumerable<UserDB> result = await _context.ScanAsync<UserDB>(new List<ScanCondition>()).GetRemainingAsync();

            return result;
        }

        public async Task<UserDB> GetUserByID(string id)
        {
            var result = await _context.LoadAsync<UserDB>(id);

            return result;
        }

        public async Task<UserDB> GetUserByEmail(string email)
        {
            var config = new DynamoDBOperationConfig
            {
                IndexName = "email-index",
            };

            IEnumerable<UserDB> result = await _context.QueryAsync<UserDB>(email, config).GetRemainingAsync();

            return result.FirstOrDefault();
        }
    }
}
