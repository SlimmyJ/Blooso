using System.Collections.Generic;

using Blooso.Models;

namespace Blooso.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);

        User GetCurrentlyLoggedInUser();

        void SetCurrentlyLoggedInUser(int id);

        List<User> GetSearchResults(string queryString);

        bool DoesUserExist(int id);

        List<User> GetMatchResults();

        List<User> GetAllUsers();

        void AddMessageToUserFeed(int userDetailId, Message wallmessage);
    }
}