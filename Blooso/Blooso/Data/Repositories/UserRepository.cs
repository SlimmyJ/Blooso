using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Blooso.Models;

using Microsoft.EntityFrameworkCore;

namespace Blooso.Data.Repositories
{
    //{
    //    public class UserRepository : IUserRepository
    //    {
    //        public UserRepository()
    //        {
    //            _dummyData = new DummyData();
    //            _userlist = FillListWithBogusData();
    //        }

    //        public User CurrentlyLoggedInUser { get; set; }

    //        public User GetCurrentlyLoggedInUser()
    //        {
    //            return CurrentlyLoggedInUser;
    //        }

    //        public void SetCurrentlyLoggedInUser(int id)
    //        {
    //            CurrentlyLoggedInUser = id == 0 ? new User() : GetUser(id);
    //        }

    //        public User GetUser(int id)
    //        {
    //            return _userlist.FirstOrDefault(x => x.Id == id);
    //        }

    //        public List<User> GetSearchResults(string queryString)
    //        {
    //            var normalizedQuery = queryString?.ToLower() ?? "";
    //            return GetMatchResults().Where(f => f.ToString().ToLowerInvariant().Contains(normalizedQuery)).ToList();
    //        }

    //        public List<User> GetMatchResults()
    //        {
    //            return _userlist
    //                .Where(user => user.Id != CurrentlyLoggedInUser.Id)
    //                .Where(user => user.IsInfected == CurrentlyLoggedInUser.IsInfected)
    //                .Where(user =>
    //                    CountOverlapInActivitiesList(user.ActivityList) > 4 && CountOverlapInTagsList(user.UserTags) > 4)
    //                .ToList();
    //        }

    //        public bool DoesUserExist(int id, string password)
    //        {
    //            return _userlist.Any(user => user.Id == id && user.Password == password);
    //        }

    //        public static UserRepository GetRepository()
    //        {
    //            return _userRepository ?? (_userRepository = new UserRepository());
    //        }

    //        public int CountOverlapInTagsList(List<Tags> list)
    //        {
    //            var overlap = list.Intersect(CurrentlyLoggedInUser.UserTags);
    //            var result = overlap.Count();
    //            return result;
    //        }

    //        public int CountOverlapInActivitiesList(List<Activities> list)
    //        {
    //            var overlap = list.Intersect(CurrentlyLoggedInUser.ActivityList);
    //            var result = overlap.Count();
    //            return result;
    //        }

    //        private List<User> FillListWithBogusData()
    //        {
    //            return _dummyData.GenerateDummyData();
    //        }
    //    }
    //}

    public class UserRepository : IUserRepository
    {
        private List<User> _userList;

        private UserRepository()
        {
            GetAllUsers();
        }

        public static UserRepository Instance { get; } = GetRepository();

        public User CurrentlyLoggedInUser { get; set; }

        public int CountOverlapInActivitiesList(List<Activity> list)
        {
            List<int> activitiesIds = GetActivityIdList(list);
            IEnumerable<int> overlap =
                activitiesIds.Intersect(GetActivityIdList(CurrentlyLoggedInUser.Activities.ToList()));
            int result = overlap.Count();
            return result;
        }

        public int CountOverlapInTagsList(List<Tag> list)
        {
            List<int> tagsIds = GetTagIdList(list);

            IEnumerable<int> overlap = tagsIds.Intersect(GetTagIdList(CurrentlyLoggedInUser.Tags.ToList()));

            int result = overlap.Count();
            return result;
        }

        public bool DoesUserExist(int id, string password)
        {
            return _userList.Any(user => user.UserId == id && user.Password == password);
        }

        public List<Activity> GetAllActivities()
        {
            using var dbContext = new BloosoContext();
            return dbContext.Activities.ToList();
        }

        public List<Tag> GetAllTags()
        {
            using var dbContext = new BloosoContext();

            return dbContext.Tags.ToList();
        }

        public async void GetAllUsers()
        {
            await using var dbContext = new BloosoContext();

            _userList = new List<User>(
                dbContext.Users
                    .Include(x => x.Activities)
                    .Include(x => x.Tags)
                    .Include(x => x.UserFeedMessages)
                    .Include(x => x.FriendList));
        }

        public User GetCurrentlyLoggedInUser() => CurrentlyLoggedInUser;

        public List<User> GetMatchResults()
        {
            return _userList.Where(user => user.UserId != CurrentlyLoggedInUser.UserId)
                .Where(user => user.IsInfected == CurrentlyLoggedInUser.IsInfected)
                .Where(
                    user => CountOverlapInActivitiesList(user.Activities.ToList()) > 4
                            && CountOverlapInTagsList(user.Tags.ToList()) > 4)
                .ToList();
        }

        public List<User> GetSearchResults(string queryString)
        {
            string normalizedQuery = queryString?.ToLower() ?? string.Empty;
            return GetMatchResults().Where(f => f.ToString().ToLowerInvariant().Contains(normalizedQuery)).ToList();
        }

        public User GetUser(int id)
        {
            return _userList.FirstOrDefault(x => x.UserId == id);
        }

        public void SetCurrentlyLoggedInUser(int id)
        {
            CurrentlyLoggedInUser = id == 0 ? new User() : GetUser(id);
        }

        public async Task UpdateUser(User user)
        {
            await using var dbContext = new BloosoContext();
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
        }

        internal static UserRepository GetRepository() => new();

        public List<int> GetActivityIdList(List<Activity> list)
        {
            var result = new List<int>();

            foreach (Activity item in list)
            {
                result.Add(item.ActivityId);
            }

            return result;
        }

        public List<int> GetTagIdList(List<Tag> list)
        {
            var result = new List<int>();

            foreach (Tag item in list)
            {
                result.Add(item.TagId);
            }

            return result;
        }
    }
}