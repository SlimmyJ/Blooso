namespace Blooso.Repositories
{
    #region

    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Blooso.Data;
    using Blooso.Interfaces;
    using Blooso.Models;

    using Microsoft.EntityFrameworkCore;

    #endregion

    #endregion

    public class UserRepository : IUserRepository
    {
        private static UserRepository _userRepository;

        private readonly DummyData _dummyData;

        private readonly List<User> _userList;

        private UserRepository()
        {
            this._dummyData = new DummyData();

            // _userList = FillListWithBogusData();
            this.FillDBOneTime();
            this.FillUsersWithActivitiesAndTags();
            this._userList = this.GetAllUsers();
        }

        public User CurrentlyLoggedInUser { get; set; }

        public static UserRepository GetRepository() => _userRepository ?? (_userRepository = new UserRepository());

        public bool DoesUserExist(int id, string password)
        {
            return this._userList.Any(user => user.Id == id && user.Password == password);
        }

        public List<User> GetAllUsers()
        {
            using (var dbContext = new BloosoContext())
            {
                return dbContext.Users.Include(x => x.Activities).Include(x => x.Tags).Include(x => x.UserFeedMessages)
                    .Include(x => x.FriendList).ToList();
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

        public User GetCurrentlyLoggedInUser() => this.CurrentlyLoggedInUser;

        public List<User> GetMatchResults()
        {
            // TODO: Uncomment relation after fixing tags and activities
            return this._userList.Where(user => user.Id != this.CurrentlyLoggedInUser.Id)
                .Where(user => user.IsInfected == this.CurrentlyLoggedInUser.IsInfected)

                // .Where(user => CountOverlapInActivitiesList(user.Activities.ToList()) > 4
                // && CountOverlapInTagsList(user.Tags.ToList()) > 4)
                .ToList();
        }

        public int CountOverlapInActivitiesList(List<Activity> list)
        {
            List<int> activitiesIds = this.GetActivityIdList(list);

            IEnumerable<int> overlap =
                activitiesIds.Intersect(this.GetActivityIdList(this.CurrentlyLoggedInUser.Activities.ToList()));
            int result = overlap.Count();
            return result;
        }

        public int CountOverlapInTagsList(List<Tag> list)
        {
            List<int> tagsIds = this.GetTagIdList(list);

            IEnumerable<int> overlap = tagsIds.Intersect(this.GetTagIdList(this.CurrentlyLoggedInUser.Tags.ToList()));

            int result = overlap.Count();
            return result;
        }

        // GENERIC?! But how????!
        public List<int> GetActivityIdList(List<Activity> list)
        {
            var result = new List<int>();

            foreach (Activity item in list)
            {
                result.Add(item.Id);
            }

            return result;
        }

        public List<int> GetTagIdList(List<Tag> list)
        {
            var result = new List<int>();

            foreach (Tag item in list)
            {
                result.Add(item.Id);
            }

            return result;
        }

        public List<User> GetSearchResults(string queryString)
        {
            string normalizedQuery = queryString?.ToLower() ?? string.Empty;
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

        /*
         * 
         * * Databasa stuff
         * *
         */
        // TODO: Put this in Seed.
        private async void FillDBOneTime()
        {
            using (var dbContext = new BloosoContext())
            {
                if (!dbContext.Users.Any())
                {
                    List<User> users = this.FillListWithBogusData();
                    await dbContext.Users.AddRangeAsync(users);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private List<User> FillListWithBogusData() => this._dummyData.GenerateDummyData();

        // async task -> warnings
        private async void FillUsersWithActivitiesAndTags()
        {
            using (var dbContext = new BloosoContext())
            {
                List<User> users = this.GetAllUsers();

                foreach (User user in users)
                {
                    user.Activities = this.GetRandomActivities(10);
                    user.Tags = this.GetRandomUserTags(12);
                }

                dbContext.Users.UpdateRange(users);
                await dbContext.SaveChangesAsync();
            }
        }

        private List<Activity> GetRandomActivities(int amount)
        {
            var rand = new Random();

            // var newList = new List<Activity>();
            List<Activity> newList = this.GetAllActivities();

            return newList.OrderBy(x => rand.Next()).Take(amount).ToList();
        }

        private List<Tag> GetRandomUserTags(int amount)
        {
            var rand = new Random();

            // var newList = new List<Tag>();
            List<Tag> newList = this.GetAllTags();

            return newList.OrderBy(x => rand.Next()).Take(amount).ToList();
        }
    }
}