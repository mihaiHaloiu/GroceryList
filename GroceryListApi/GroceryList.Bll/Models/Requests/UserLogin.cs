using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryList.Bll.Models.Requests
{
    public class UserLogin
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
