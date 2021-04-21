using System;
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
        private DummyData _dummyData;

        private UserRepository()
        {
            _dummyData = new DummyData();
            _userlist = FillListWithBogusData();
        }

        private List<User> _userlist;

        public User CurrentlyLoggedInUser { get; set; }

        /// <summary>
        ///           Singleton pattern for UserRepository
        /// </summary>
        /// <returns>
        /// </returns>

        public static UserRepository GetRepository() => _userRepository ?? (_userRepository = new UserRepository());

        public User GetCurrentlyLoggedInUser()
        {
            return CurrentlyLoggedInUser;
        }

        public void SetCurrentlyLoggedInUser(int id)
        {
            if (id == 0)
            {
                CurrentlyLoggedInUser = new User();
            }
            else
            {
                CurrentlyLoggedInUser = GetUser(id);
            }
        }

        public List<User> GetAllUsers()
        {
            return _userlist;
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
            var matches = new List<User>();

            foreach (var user in _userlist)
            {
                if(user.Id != CurrentlyLoggedInUser.Id)
                {
                    if (user.IsInfected == CurrentlyLoggedInUser.IsInfected)
                    {
                        if (CountOverlapInActivitiesList(user.ActivityList) > 4 && CountOverlapInTagsList(user.UserTags) > 4)
                        {
                            matches.Add(user);
                        }
                    }
                }                
            }

            return matches;
        }

        public int CountOverlapInTagsList(List<Tags> list)
        {
            IEnumerable<Tags> overlap = list.Intersect(CurrentlyLoggedInUser.UserTags);
            var result = overlap.Count();
            return result;
        }
        public int CountOverlapInActivitiesList(List<Activities> list)
        {
            IEnumerable<Activities> overlap = list.Intersect(CurrentlyLoggedInUser.ActivityList);
            var result = overlap.Count();
            return result;
        }

        public bool DoesUserExist(int id)
        {
            foreach (var user in _userlist)
            {
                if (user.Id == id)
                    return true;
            }

            return false;
        }

        private List<User> FillListWithBogusData()
        {
            return _dummyData.GenerateDummyData();
        }

        private List<User> FillListWithDummyData()
        {
            return new List<User>
            {
                new User
                    {
                        Id = 1,
                        Name = "Jeroen",
                        DateOfBirth = DateTime.Today,
                        Sex = 'm',
                        UserLocation = new UserLocation(),
                        FriendsList = new List<User>(),
                        IsInfected = true,
                        IsVaccinated = false,
                        ProfileCommentsList = new List<Message>(),
                        ActivityList = new List<Activities>()
                        {
                            Activities.Running,
                            Activities.Basketball,
                            Activities.Lacrosse,
                            Activities.Minigolf,
                            Activities.Sauna,
                        },
                        UserTags = new List<Tags>() {Tags.Arts, Tags.Books, Tags.Wine}
                    },

                    new User
                    {
                        Id = 2,
                        Name = "Bassie",
                        DateOfBirth = DateTime.Today,
                        Sex = 'm',
                        UserLocation = new UserLocation(),
                        FriendsList = new List<User>(),
                        IsInfected = true,
                        IsVaccinated = false,
                        ProfileCommentsList = new List<Message>(),
                        ActivityList = new List<Activities>()    {
                            Activities.Running,
                            Activities.Basketball,
                            Activities.Lacrosse,
                            Activities.Squash,
                            Activities.Sauna,
                            Activities.Yoga,
                        },
                        UserTags = new List<Tags>() {Tags.Smoker, Tags.Books, Tags.Wine}
                    },

                    new User
                    {
                        Id = 3,
                        Name = "Andrea",
                        DateOfBirth = DateTime.Today,
                        Sex = 'f',
                        UserLocation = new UserLocation(),
                        FriendsList = new List<User>(),
                        IsInfected = true,
                        IsVaccinated = false,
                        ProfileCommentsList = new List<Message>(),
                        ActivityList = new List<Activities>()  {
                            Activities.Running,
                            Activities.Handball,
                            Activities.Lacrosse,
                            Activities.Squash,
                            Activities.Padel,
                            Activities.Yoga,
                        },
                        UserTags = new List<Tags>() {Tags.Arts, Tags.Larping, Tags.Books, Tags.Wine}
                    },

                    new User
                        {
                        Id = 4,
                        Name = "Libelle",
                        DateOfBirth = DateTime.Today,
                        Sex = 'f',
                        UserLocation = new UserLocation(9000),
                        FriendsList = new List<User>(),
                        IsInfected = false,
                        IsVaccinated = false,
                        ProfileCommentsList = new List<Message>(),
                        ActivityList = new List<Activities>() {
                            Activities.Running,
                            Activities.Handball,
                            Activities.Lacrosse,
                            Activities.Squash,
                            Activities.Padel,
                            Activities.Yoga,
                        },
                        UserTags = new List<Tags>(){Tags.Arts, Tags.Furry, Tags.Books, Tags.Wine},
                        }
            };
        }
    }
}