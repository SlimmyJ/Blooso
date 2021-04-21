﻿using System.Collections.Generic;

using Blooso.Models;

namespace Blooso.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();

        User GetUser(int id);

        User GetCurrentlyLoggedInUser();

        void SetCurrentlyLoggedInUser(int id);

        List<User> GetSearchResults(string queryString);
        bool DoesUserExist(int id);
    }
}