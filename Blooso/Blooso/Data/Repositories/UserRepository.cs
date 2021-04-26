using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Display.Core;
using Blooso.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Blooso.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> _userList;

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

        public List<Activity> GetAllActivities()
        {
            using var dbContext = BloosoContext.CreateInstance();
            return dbContext.Activities.ToList();
        }

        public List<Tag> GetAllTags()
        {
            using var dbContext = BloosoContext.CreateInstance();

            return dbContext.Tags.ToList();
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
            await using var dbContext = BloosoContext.CreateInstance();
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
        }

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

        public async Task<List<User>> GetAllUsers()
        {
            await using (var dbContext = BloosoContext.CreateInstance())
            {
                var userlist = dbContext.Users
                    .Include(x => x.UserId)
                    .Include(x => x.Activities).ThenInclude(x => x.ActivityUser)
                    .Include(x => x.Tags).ThenInclude(x => x.TagUsers)
                    .Include(x => x.FriendList)
                    .Include(x => x.Name)
                    .Include(x => x.Password).ToList();

                _userList = userlist;
            }

            return _userList;
        }

        public async Task<bool> DoesUserExist(int id, string password)
        {
            await using (var dbContext = BloosoContext.CreateInstance())
            {
                return _userList.Any(user => user.UserId == id);
            }
        }
    }
}