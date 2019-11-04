using GroceryList.Bll.Models;
using GroceryList.Bll.Models.Reponses;
using GroceryList.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroceryList.Bll.Mappers
{
    public class UserMapper : IUserMapper
    {
        public IEnumerable<User> UserDBToMovieModel(IEnumerable<UserDB> items)
        {
            return items.Select(UserDBToMovieModel);
        }

        public User UserDBToMovieModel(UserDB userDB)
        {
            if(userDB == null)
            {
                return null;
            }

            return new User
            {
                ID = userDB.id,
                Name = userDB.name,
                Email = userDB.email,
                Password = userDB.password
            };
        }

        public UserResponse UserToUserResponse(User user)
        {

            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                ID = user.ID,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
