using Amazon.DynamoDBv2.DataModel;

namespace GroceryList.DAL.Models
{
    [DynamoDBTable("users")]
    public class UserDB
    {
        [DynamoDBHashKey]
        public string id { get; set; }

        public string name { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey]
        public string email { get; set; }

        public string password { get; set; }

    }
}
