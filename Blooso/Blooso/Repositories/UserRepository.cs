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
            throw new NotImplementedException();
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