﻿using Blooso.Models;
using System.Collections.Generic;


using System.Threading.Tasks;

namespace Blooso.Interfaces
{
    public interface IUserRepository
    {
        User CurrentlyLoggedInUser { get; set; }

        User GetCurrentlyLoggedInUser();

        void SetCurrentlyLoggedInUser(int id);

        User GetUser(int id);

        List<User> GetMatchResults();

        List<User> GetSearchResults(string queryString);

        bool DoesUserExist(int id, string password);

        int CountOverlapInTagsList(List<Models.Tag> list);

        int CountOverlapInActivitiesList(List<Activity> list);

        List<User> GetAllUsers();

        Task UpdateUser(User user);
        List<Activity> GetAllActivities();
        List<Models.Tag> GetAllTags();
    }
}