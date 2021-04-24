namespace Blooso.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Interfaces;
    using Models;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : IUserRepository
    {
        private readonly DummyData _dummyData;

        private readonly List<User> _userList;

        private UserRepository()
        {
            _dummyData = new DummyData();
            FillDbOneTime();

            FillUsersWithActivitiesAndTags();
            _userList = GetAllUsers();
        }

        private static UserRepository _userRepository { get; set; } = new UserRepository();

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
            return _userList.Any(user => user.Id == id && user.Password == password);
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

        public List<User> GetAllUsers()
        {
            using (var dbContext = new BloosoContext())
            {
                return dbContext.Users.Include(x => x.Activities).Include(x => x.Tags).Include(x => x.UserFeedMessages)
                    .Include(x => x.FriendList).ToList();
            }
        }

        public User GetCurrentlyLoggedInUser() => CurrentlyLoggedInUser;

        public List<User> GetMatchResults()
        {
            // TODO: Uncomment relation after fixing tags and activities
            return _userList.Where(user => user.Id != CurrentlyLoggedInUser.Id)
                .Where(user => user.IsInfected == CurrentlyLoggedInUser.IsInfected)

                //.Where(user => CountOverlapInActivitiesList(user.Activities.ToList()) > 4
                //            && CountOverlapInTagsList(user.Tags.ToList()) > 4)
                .ToList();
        }

        public List<User> GetSearchResults(string queryString)
        {
            string normalizedQuery = queryString?.ToLower() ?? string.Empty;
            return GetMatchResults().Where(f => f.ToString().ToLowerInvariant().Contains(normalizedQuery)).ToList();
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

        public static UserRepository GetRepository() => _userRepository ?? (_userRepository = _userRepository);

        //GENERIC?! But how????!
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

        // TODO: Put this in Seed.
        private async void FillDbOneTime()
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

        private List<User> FillListWithBogusData() => _dummyData.GenerateUserList();

        private async void FillUsersWithActivitiesAndTags()
        {
            using (var dbContext = new BloosoContext())
            {
                List<User> users = GetAllUsers();

                foreach (User user in users)
                {
                    List<Activity> randomactivities = GetRandomActivities(10);
                    foreach (Activity activity in randomactivities)
                    {
                        user.Activities.Add(activity);
                    }

                    foreach (Tag tags in GetRandomUserTags(12))
                    {
                        user.Tags.Add(tags);
                    }
                }

                dbContext.Users.UpdateRange(users);

                await dbContext.SaveChangesAsync();
            }
        }

        private List<Activity> GetRandomActivities(int amount)
        {
            var rand = new Random();
            List<Activity> newList = GetAllActivities();
            return newList.OrderBy(x => rand.Next()).Take(amount).ToList();
        }

        private List<Tag> GetRandomUserTags(int amount)
        {
            var rand = new Random();
            List<Tag> newList = GetAllTags();
            return newList.OrderBy(x => rand.Next()).Take(amount).ToList();
        }
    }
}