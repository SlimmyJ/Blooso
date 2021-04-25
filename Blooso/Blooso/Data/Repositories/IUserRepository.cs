using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blooso.Models;

namespace Blooso.Data.Repositories
{
    public interface IUserRepository
    {
        User CurrentlyLoggedInUser { get; set; }

        void GetAllUsers();

        User GetCurrentlyLoggedInUser();

        void SetCurrentlyLoggedInUser(int id);

        User GetUser(int id);

        List<User> GetMatchResults();

        List<User> GetSearchResults(string queryString);

        bool DoesUserExist(int id, string password);

        int CountOverlapInTagsList(List<Tag> list);

        int CountOverlapInActivitiesList(List<Activity> list);

        Task UpdateUser(User user);

        List<Activity> GetAllActivities();

        List<Tag> GetAllTags();
    }
}