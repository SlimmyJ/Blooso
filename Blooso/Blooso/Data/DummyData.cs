using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Blooso.Models;

using Bogus;

namespace Blooso.Data
{
    public class DummyData
    {
        public List<User> GenerateDummyData()
        {
            var dummyList = new List<User>();
            var userFaker = new Faker<User>()
                .RuleFor(x => x.Name, x => x.Person.FullName)
                .RuleFor(x => x.Sex, x => x.PickRandom("Male", "Female", "Gender Fluid"))
                .RuleFor(x => x.IsVaccinated, x => x.Random.Bool())
                .RuleFor(x => x.IsInfected, x => x.Random.Bool())
                .RuleFor(x => x.UserLocation, x => x.Person.Address.City)
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth)
                .RuleFor(x => x.ActivityList, new List<Activities>())
                .RuleFor(x => x.UserTags, new List<Tags>())
                .RuleFor(x => x.FriendList, new ObservableCollection<User>())
                .RuleFor(x => x.UserFeedMessages, new ObservableCollection<Message>());

            //var user = JsonConvert.SerializeObject(userFaker.Generate());
            //Debug.WriteLine($" TEST:: {user}");

            for (var i = 0; i < 20; i++)
            {
                var temp = userFaker.Generate();
                temp.Id = i + 1;
                temp.UserPicture = $"a{i}.jpg";
                temp.ActivityList = GetRandomActivities(10);
                temp.UserTags = GetRandomUserTags(12);
                temp.ShortBio = "";

                dummyList.Add(temp);
            }

            return dummyList;
        }

        public List<Activities> GetRandomActivities(int amount)
        {
            var rand = new Random();
            var newList = Enum.GetValues(typeof(Activities)).Cast<Activities>().ToList();
            var randomActivities = newList.OrderBy(x => rand.Next()).Take(amount).ToList();

            return randomActivities;
        }

        public List<Tags> GetRandomUserTags(int amount)
        {
            var rand = new Random();
            var newList = Enum.GetValues(typeof(Tags)).Cast<Tags>().ToList();
            var randomUserTags = newList.OrderBy(x => rand.Next()).Take(amount).ToList();

            return randomUserTags;
        }
    }
}