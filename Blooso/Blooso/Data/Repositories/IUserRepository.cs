using System.Collections.Generic;
using System.Threading.Tasks;

using Blooso.Models;

namespace Blooso.Data.Repositories
{
    public interface IUserRepository
    {
        User CurrentlyLoggedInUser { get; set; }

        int CountOverlapInActivitiesList(List<Activity> list);

        int CountOverlapInTagsList(List<Tag> list);

        Task<bool> DoesUserExist(int id, string password);

        List<Activity> GetAllActivities();

        List<Tag> GetAllTags();

        Task<List<User>> GetAllUsers();

        User GetCurrentlyLoggedInUser();

        List<User> GetMatchResults();

        List<User> GetSearchResults(string queryString);

        User GetUser(int id);

        void SetCurrentlyLoggedInUser(int id);

        Task UpdateUser(User user);

        List<int> GetActivityIdList(List<Activity> list);

        List<int> GetTagIdList(List<Tag> list);
    }
}