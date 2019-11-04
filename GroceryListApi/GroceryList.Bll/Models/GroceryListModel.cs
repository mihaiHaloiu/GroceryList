using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryList.Bll.Models
{
    public class GroceryListModel
    {
        public string ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<string> Items { get; set; }
    }
}
