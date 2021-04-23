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
            this._dummyData = new DummyData();

            // _userList = FillListWithBogusData();
            this.FillDBOneTime();
            this._userList = this.GetAllUsers();
        }

        public User CurrentlyLoggedInUser { get; set; }

        public static UserRepository GetRepository()
        {
            return _userRepository ?? (_userRepository = new UserRepository());
        }

        public int CountOverlapInActivitiesList(List<Activities> list)
        {
            var overlap = list.Intersect(this.CurrentlyLoggedInUser.Activities);
            var result = overlap.Count();
            return result;
        }

        public int CountOverlapInTagsList(List<Tags> list)
        {
            var overlap = list.Intersect(this.CurrentlyLoggedInUser.Tags);
            var result = overlap.Count();
            return result;
        }

        public bool DoesUserExist(int id, string password)
        {
            return this._userList.Any(user => user.Id == id && user.Password == password);
        }

        public List<User> GetAllUsers()
        {
            using (var dbContext = new BloosoContext())
            {
                return dbContext.Users.Include(x => x.Activities).Include(x => x.Tags).Include(x => x.UserFeedMessages)
                    .ToList();
            }
        }

        public User GetCurrentlyLoggedInUser()
        {
            return this.CurrentlyLoggedInUser;
        }

        public List<User> GetMatchResults()
        {
            // TODO: Uncomment relation after fixing tags and activities
            return this._userList.Where(user => user.Id != this.CurrentlyLoggedInUser.Id)
                .Where(user => user.IsInfected == this.CurrentlyLoggedInUser.IsInfected).Where(
                    user => this.CountOverlapInActivitiesList(user.ActivityList) > 4
                            && this.CountOverlapInTagsList(user.UserTags) > 4).ToList();
        }

        public List<User> GetSearchResults(string queryString)
        {
            var normalizedQuery = queryString?.ToLower() ?? string.Empty;
            return this.GetMatchResults().Where(f => f.ToString().ToLowerInvariant().Contains(normalizedQuery))
                .ToList();
        }

        public User GetUser(int id)
        {
            return this._userList.FirstOrDefault(x => x.Id == id);
        }

        public void SetCurrentlyLoggedInUser(int id)
        {
            this.CurrentlyLoggedInUser = id == 0 ? new User() : this.GetUser(id);
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
                    var users = this.FillListWithBogusData();
                    await dbContext.Users.AddRangeAsync(users);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private List<User> FillListWithBogusData()
        {
            return this._dummyData.GenerateDummyData();
        }
    }
}