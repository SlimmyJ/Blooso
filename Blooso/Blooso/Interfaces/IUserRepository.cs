namespace Blooso.Interfaces
{
    #region

    using System.Collections.Generic;

    using Blooso.Models;

    #endregion

    public interface IUserRepository
    {
        bool DoesUserExist(int id);

        User GetCurrentlyLoggedInUser();

        List<User> GetMatchResults();

        List<User> GetSearchResults(string queryString);

        User GetUser(int id);

        void SetCurrentlyLoggedInUser(int id);
    }
}