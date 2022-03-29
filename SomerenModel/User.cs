﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool? Admin { get; set; }

        public  UserQuestion  question{ get; set; }
    


        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
