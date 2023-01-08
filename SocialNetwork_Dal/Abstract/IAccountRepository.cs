﻿using SocialNetwork_Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_Dal.Abstract
{
    public interface IAccountRepository
    {
        bool RegisterUser(User user);

        User LoginUser(UserLoginCheck user);
        bool EmailExists(string Email);
    }
}
