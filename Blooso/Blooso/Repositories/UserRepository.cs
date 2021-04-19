using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Blooso.Interfaces;
using Blooso.Models;

namespace Blooso.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static UserRepository _userRepository;

        private UserRepository()
        {
            _userlist = FillListWithDummyData();
        }

        private List<User> _userlist;

        /// <summary>
        ///           Singleton pattern for UserRepository
        /// </summary>
        /// <returns>
        /// </returns>

        public static UserRepository GetRepository() => _userRepository ?? (_userRepository = new UserRepository());

        public List<User> GetAllUsers()
        {
            return _userlist;
        }

        public User GetUser(int id)
        {
            return _userlist.FirstOrDefault(x => x.Id == id);
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
                        Messages = new List<Message>(),
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
                        Messages = new List<Message>(),
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
                        Messages = new List<Message>(),
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
                        UserLocation = new UserLocation(),
                        FriendsList = new List<User>(),
                        IsInfected = true,
                        IsVaccinated = false,
                        Messages = new List<Message>(),
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