using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blooso.Models;

namespace Blooso.Data.Repositories
{
    #region

    #endregion

    public interface IUserRepository
    {
        User CurrentlyLoggedInUser { get; set; }

        User GetCurrentlyLoggedInUser();

        void SetCurrentlyLoggedInUser(int id);

        User GetUser(int id);

        List<User> GetMatchResults();

        List<User> GetSearchResults(string queryString);

        bool DoesUserExist(int id, string password);

        int CountOverlapInTagsList(List<Tag> list);

        int CountOverlapInActivitiesList(List<Activity> list);

        List<User> GetAllUsers();

        Task UpdateUser(User user);

        List<Activity> GetAllActivities();

        List<Tag> GetAllTags();
    }
}