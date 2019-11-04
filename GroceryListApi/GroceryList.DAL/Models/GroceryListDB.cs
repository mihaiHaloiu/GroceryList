using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

namespace GroceryList.DAL.Models
{
  
    [DynamoDBTable("groceryLists")]
    public class GroceryListDB
    {
        [DynamoDBHashKey]
        public string id { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public List<string> items { get; set; }            
    }
   
}
