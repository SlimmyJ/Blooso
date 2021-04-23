namespace Blooso.Repositories
{
    #region

    using System.Collections.Generic;
    using System.Linq;

    using Blooso.Data;
    using Blooso.Interfaces;
    using Blooso.Models;

    #endregion

    public class UserRepository : IUserRepository
    {
        private static UserRepository userRepository;

        private readonly DummyData _dummyData;

        private readonly List<User> _userlist;

        public UserRepository()
        {
            this._dummyData = new DummyData();
            this._userlist = this.FillListWithBogusData();
        }

        public User CurrentlyLoggedInUser { get; set; }

        public static UserRepository GetRepository()
        {
            return userRepository ?? (userRepository = new UserRepository());
        }

        public int CountOverlapInActivitiesList(IEnumerable<Activities> list)
        {
            var overlap = list.Intersect(this.CurrentlyLoggedInUser.ActivityList);
            var result = overlap.Count();
            return result;
        }

        public int CountOverlapInTagsList(IEnumerable<Tags> list)
        {
            var overlap = list.Intersect(this.CurrentlyLoggedInUser.UserTags);
            var result = overlap.Count();
            return result;
        }

        public bool DoesUserExist(int id)
        {
            foreach (var user in this._userlist)
            {
                if (user.Id == id)
                {
                    return true;
                }
            }

            return false;
        }

        public User GetCurrentlyLoggedInUser()
        {
            return this.CurrentlyLoggedInUser;
        }

        public List<User> GetMatchResults()
        {
            return this._userlist.Where(user => !user.Id.Equals(this.CurrentlyLoggedInUser.Id))
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
            return this._userlist.FirstOrDefault(x => x.Id == id);
        }

        public void SetCurrentlyLoggedInUser(int id)
        {
            this.CurrentlyLoggedInUser = id == 0 ? new User() : this.GetUser(id);
        }

        private List<User> FillListWithBogusData()
        {
            return this._dummyData.GenerateDummyData();
        }
    }
}