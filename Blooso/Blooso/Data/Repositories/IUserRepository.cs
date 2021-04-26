#region

using System.Collections.Generic;
using System.Threading.Tasks;

using Blooso.Models;

#endregion

namespace Blooso.Data.Repositories
{
    public interface IUserRepository
    {
        List<User> _userList { get; set; }

        User CurrentlyLoggedInUser { get; set; }

        int CountOverlapInActivitiesList(List<Activity> list);

        int CountOverlapInTagsList(List<Tag> list);

        List<Activity> GetAllActivities();

        List<Tag> GetAllTags();

        User GetCurrentlyLoggedInUser();

        List<User> GetMatchResults();

        List<User> GetSearchResults(string queryString);

        User GetUser(int id);

        void SetCurrentlyLoggedInUser(int id);

        Task UpdateUser(User user);

        List<int> GetActivityIdList(List<Activity> list);

        List<int> GetTagIdList(List<Tag> list);

        Task<List<User>> GetAllUsers();

        Task<bool> DoesUserExist(int id, string password);
    }
}