﻿namespace Blooso.Repositories
{
    using System;
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

        private UserRepository()
        {
            _dummyData = new DummyData();

            // _userList = FillListWithBogusData();
            FillDBOneTime();
            FillUsersWithActivitiesAndTags();
            _userList = GetAllUsers();            
        }

        public User CurrentlyLoggedInUser { get; set; }

        public static UserRepository GetRepository()
        {
            return _userRepository ?? (_userRepository = new UserRepository());
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
                    .Include(x => x.FriendList)
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
                //.Where(user => CountOverlapInActivitiesList(user.Activities.ToList()) > 4
                //            && CountOverlapInTagsList(user.Tags.ToList()) > 4)
                .ToList();
        }        

        public int CountOverlapInActivitiesList(List<Activity> list)
        {
            var activitiesIds = GetActivityIdList(list);

            var overlap = activitiesIds.Intersect(GetActivityIdList(CurrentlyLoggedInUser.Activities.ToList()));
            var result = overlap.Count();
            return result;
        }

        public int CountOverlapInTagsList(List<Tag> list)
        {
            var tagsIds = GetTagIdList(list);

            var overlap = tagsIds.Intersect(GetTagIdList(CurrentlyLoggedInUser.Tags.ToList()));

            var result = overlap.Count();
            return result;
        }
        //GENERIC?! But how????!
        public List<int> GetActivityIdList(List<Activity> list)
        {
            var result = new List<int>();

            foreach (var item in list)
            {
                result.Add(item.Id);
            }
            return result;
        }
        public List<int> GetTagIdList(List<Tag> list)
        {
            var result = new List<int>();

            foreach (var item in list)
            {
                result.Add(item.Id);
            }
            return result;
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
        //async task -> warnings
        private async void FillUsersWithActivitiesAndTags()
        {
            using (var dbContext = new BloosoContext())
            {
                var users = GetAllUsers();
                //var activities = GetAllActivities();
                //var tags = GetAllTags();

                foreach (var user in users)
                {
                    var randomactivities = GetRandomActivities(10);
                    foreach (var activity in randomactivities)
                    {
                        user.Activities.Add(activity);
                    }
                    //user.Activities = GetRandomActivities(10);
                    //foreach (var activity in activities)
                    //{
                    //    activity.Users.Add(user);
                    //}
                    foreach (var tags in GetRandomUserTags(12))
                    {
                        user.Tags.Add(tags);
                    }
                    //user.Tags = GetRandomUserTags(12);
                    //foreach (var tag in tags)
                    //{
                    //    tag.Users.Add(user);
                    //}
                    //await UpdateUser(user);
                }

                dbContext.Users.UpdateRange(users);
                //dbContext.Activities.UpdateRange(activities);
                //dbContext.Tags.UpdateRange(tags);
                await dbContext.SaveChangesAsync();
            }           

        }
        private List<Activity> GetRandomActivities(int amount)
        {
            var rand = new Random();
            //var newList = new List<Activity>();
            var newList = GetAllActivities();

            return newList.OrderBy(x => rand.Next()).Take(amount).ToList();
        }
        private List<Tag> GetRandomUserTags(int amount)
        {
            var rand = new Random();
            //var newList = new List<Tag>();
            var newList = GetAllTags();

            return newList.OrderBy(x => rand.Next()).Take(amount).ToList();
        }

    }
}