using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenLogic
{
    public class UserService
    {
        UserDao userdb;

        public UserService()
        {
            userdb = new UserDao();
        }

        public bool? ValidateUser(User user)
        {
            return userdb.ValidateUserPassword(user);
        }

        public bool RegisterUser(User user)
        {
            return userdb.RegisterUser(user);
        }
    }
}
