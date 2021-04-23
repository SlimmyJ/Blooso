using System.Collections.Generic;
using System.Linq;
using Blooso.Data;
using Blooso.Interfaces;
using Blooso.Models;

namespace Blooso.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static UserRepository _userRepository;
        private readonly DummyData _dummyData;

        private readonly List<User> _userlist;

        public UserRepository()
        {
            _dummyData = new DummyData();
            _userlist = FillListWithBogusData();
        }

        public User CurrentlyLoggedInUser { get; set; }

        public User GetCurrentlyLoggedInUser()
        {
            return CurrentlyLoggedInUser;
        }

        public void SetCurrentlyLoggedInUser(int id)
        {
            CurrentlyLoggedInUser = id == 0 ? new User() : GetUser(id);
        }

        public User GetUser(int id)
        {
            return _userlist.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetSearchResults(string queryString)
        {
            var normalizedQuery = queryString?.ToLower() ?? "";
            return GetMatchResults().Where(f => f.ToString().ToLowerInvariant().Contains(normalizedQuery)).ToList();
        }

        public List<User> GetMatchResults()
        {
            return _userlist
                .Where(user => user.Id != CurrentlyLoggedInUser.Id)
                .Where(user => user.IsInfected == CurrentlyLoggedInUser.IsInfected)
                .Where(user =>
                    CountOverlapInActivitiesList(user.ActivityList) > 4 && CountOverlapInTagsList(user.UserTags) > 4)
                .ToList();
        }

        public bool DoesUserExist(int id, string password)
        {
            foreach (var user in _userlist)
                if (user.Id == id && user.Password == password)
                    return true;

            return false;
        }

        public static UserRepository GetRepository()
        {
            return _userRepository ?? (_userRepository = new UserRepository());
        }

        public int CountOverlapInTagsList(List<Tags> list)
        {
            var overlap = list.Intersect(CurrentlyLoggedInUser.UserTags);
            var result = overlap.Count();
            return result;
        }

        public int CountOverlapInActivitiesList(List<Activities> list)
        {
            var overlap = list.Intersect(CurrentlyLoggedInUser.ActivityList);
            var result = overlap.Count();
            return result;
        }

        private List<User> FillListWithBogusData()
        {
            return _dummyData.GenerateDummyData();
        }
    }
}