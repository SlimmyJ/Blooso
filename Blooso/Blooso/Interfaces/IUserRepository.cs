using Blooso.Models;
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

        List<User> GetSearchResults(string queryString);

        List<User> GetMatchResults();

        bool DoesUserExist(int id);

        int CountOverlapInTagsList(List<Tags> list);

        int CountOverlapInActivitiesList(List<Activities> list);

        List<User> GetAllUsers();

        Task UpdateUser(User user);
    }
}