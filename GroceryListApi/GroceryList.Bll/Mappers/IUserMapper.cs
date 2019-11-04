using System;
using System.Collections.Generic;
using System.Text;
using GroceryList.Bll.Models;
using GroceryList.Bll.Models.Reponses;
using GroceryList.DAL.Models;

namespace GroceryList.Bll.Mappers
{
    public interface IUserMapper
    {
        IEnumerable<User> UserDBToMovieModel(IEnumerable<UserDB> items);
        User UserDBToMovieModel(UserDB item);

        UserResponse UserToUserResponse(User user);
    }
}
