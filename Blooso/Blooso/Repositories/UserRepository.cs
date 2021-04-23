namespace Blooso.Repositories
{
    #region

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Blooso.Data;
    using Blooso.Interfaces;
    using Blooso.Models;

    using Microsoft.EntityFrameworkCore;

    #endregion

    public class UserRepository : IUserRepository
    {
        private static UserRepository _userRepository;

        private readonly DummyData _dummyData;

        private readonly List<User> _userList;

        public UserRepository()
        {
            _dummyData = new DummyData();

            // _userList = FillListWithBogusData();
            FillDBOneTime();
            _userList = GetAllUsers();
        }

        public User CurrentlyLoggedInUser { get; set; }

        public static UserRepository GetRepository()
        {
            return _userRepository ?? (_userRepository = new UserRepository());
        }

        public int CountOverlapInActivitiesList(List<Activity> list)
        {
            var overlap = list.Intersect(CurrentlyLoggedInUser.Activities);
            var result = overlap.Count();
            return result;
        }

        public int CountOverlapInTagsList(List<Tag> list)
        {
            var overlap = list.Intersect(CurrentlyLoggedInUser.Tags);
            var result = overlap.Count();
            return result;
        }

        public bool DoesUserExist(int id, string password)
        {
            return _userList.Any(user => user.Id == id && user.Password == password);
        }

        public List<User> GetAllUsers()
        {
            using (var dbContext = new BloosoContext())
            {
                return dbContext.Users.
                    Include(x => x.Activities)
                    .Include(x => x.Tags)
                    .Include(x => x.UserFeedMessages)
                    .ToList();
            }
        }

        public List<Activity> GetAllActivities()
        {
            using (var dbContext = new BloosoContext())
            {
                return dbContext.Activities.ToList();
            }
        }

        public List<Tag> GetAllTags()
        {
            using (var dbContext = new BloosoContext())
            {
                return dbContext.Tags.ToList();
            }
        }

        public User GetCurrentlyLoggedInUser()
        {
            return CurrentlyLoggedInUser;
        }

        public List<User> GetMatchResults()
        {
            // TODO: Uncomment relation after fixing tags and activities
            return _userList.Where(user => user.Id != CurrentlyLoggedInUser.Id)
                .Where(user => user.IsInfected == CurrentlyLoggedInUser.IsInfected)
                .Where(user => CountOverlapInActivitiesList(user.Activities.ToList()) > 4
                            && CountOverlapInTagsList(user.Tags.ToList()) > 4)
                .ToList();
        }

        public List<User> GetSearchResults(string queryString)
        {
            var normalizedQuery = queryString?.ToLower() ?? string.Empty;
            return GetMatchResults().Where(f => f.ToString().ToLowerInvariant().Contains(normalizedQuery))
                .ToList();
        }

        public User GetUser(int id)
        {
            return _userList.FirstOrDefault(x => x.Id == id);
        }

        public void SetCurrentlyLoggedInUser(int id)
        {
            CurrentlyLoggedInUser = id == 0 ? new User() : GetUser(id);
        }

        public async Task UpdateUser(User user)
        {
            using (var dbContext = new BloosoContext())
            {
                dbContext.Update(user);
                await dbContext.SaveChangesAsync();
            }
        }

        // TODO: Put this in Seed.
        private async void FillDBOneTime()
        {
            using (var dbContext = new BloosoContext())
            {
                if (!dbContext.Users.Any())
                {
                    var users = FillListWithBogusData();
                    await dbContext.Users.AddRangeAsync(users);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private List<User> FillListWithBogusData()
        {
            return _dummyData.GenerateDummyData();
        }
    }
}