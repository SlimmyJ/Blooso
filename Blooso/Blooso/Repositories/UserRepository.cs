using Blooso.Data;
using Blooso.Interfaces;
using Blooso.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blooso.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static UserRepository _userRepository;
        private readonly DummyData _dummyData;

        private readonly List<User> _userList;

        public UserRepository()
        {
            _dummyData = new DummyData();
            //_userList = FillListWithBogusData();
            FillDBOneTime();
            _userList = GetAllUsers();
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
            return _userList.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetSearchResults(string queryString)
        {
            var normalizedQuery = queryString?.ToLower() ?? "";
            return GetMatchResults().Where(f => f.ToString().ToLowerInvariant().Contains(normalizedQuery)).ToList();
        }

        public List<User> GetMatchResults()
        {
            // TODO: Uncomment relation after fixing tags and activities
            return _userList
                .Where(user => user.Id != CurrentlyLoggedInUser.Id)
                .Where(user => user.IsInfected == CurrentlyLoggedInUser.IsInfected)
                //.Where(user =>
                //    CountOverlapInActivitiesList(user.ActivityList) > 4 && CountOverlapInTagsList(user.UserTags) > 4)
                .ToList();
        }

        public bool DoesUserExist(int id)
        {
            return _userList.Any(user => user.Id == id);
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

        public List<User> GetAllUsers()
        {
            using (var dbContext = new BloosoContext())
            {
                return dbContext.Users
                    .Include(x => x.Activities)
                    .Include(x => x.Tags)
                    .Include(x => x.UserFeedMessages)
                    .ToList();
            }
        }

        // TODO: Put this in Seed.
        private async void FillDBOneTime()
        {
            using (var dbContext = new BloosoContext())
            {
                if (!dbContext.Users.Any())
                {
                    List<User> users = FillListWithBogusData();
                    await dbContext.Users.AddRangeAsync(users);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task UpdateUser(User user)
        {
            using (var dbContext = new BloosoContext())
            {
                dbContext.Update(user);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}