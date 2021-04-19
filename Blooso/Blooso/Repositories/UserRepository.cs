using System;
using System.Collections.Generic;

using Blooso.Models;

namespace Blooso.Repositories
    {
    public class UserRepository
        {
        private static UserRepository _userRepository;

        private UserRepository()
            {
            _userlist = GetDummyData();
            }

        private List<User> _userlist { get; set; }

        private List<User> GetDummyData()
            {
            return new List<User>
            {
                new User
                    {
                        Id = 1,
                        Name = "Jeroen",
                        DateOfBirth = DateTime.Today,
                        AreaCode = 8000,
                    },
                    new User
                    {
                        Id = 2,
                        Name = "Bassie",
                    },
                    new User
                    {
                        Id = 3,
                        Name = "Andrea",
                    },
                    new User
                    {
                        Id = 4,
                        Name = "Libelle",
                    }
            };
            }

        /// <summary>
        ///           Singleton pattern for UserRepository
        /// </summary>
        /// <returns>
        /// </returns>
        public static UserRepository GetRepository() => _userRepository ?? (_userRepository = new UserRepository());

        public List<User> GetUserList()
            {
            return _userlist;
            }
        }
    }