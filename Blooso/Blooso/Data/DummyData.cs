using System;
using System.Linq;

namespace Blooso.Data
{
    #region

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Models;
    using Bogus;

    #endregion

    public class DummyData
    {
        public List<User> GenerateUserList()
        {
            var dummyList = new List<User>();
            Faker<User> userFaker = new Faker<User>().RuleFor(x => x.Name, x => x.Person.FullName)
                .RuleFor(x => x.Sex, x => x.PickRandom("Male", "Female", "Gender Fluid"))
                .RuleFor(x => x.IsVaccinated, x => x.Random.Bool()).RuleFor(x => x.IsInfected, x => x.Random.Bool())
                .RuleFor(x => x.UserLocation, x => x.Person.Address.City)
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth).RuleFor(x => x.Activities, new List<Activity>())
                .RuleFor(x => x.Tags, new List<Tag>()).RuleFor(x => x.FriendList, new ObservableCollection<User>())
                .RuleFor(x => x.UserFeedMessages, new ObservableCollection<Message>());

            // var user = JsonConvert.SerializeObject(userFaker.Generate());
            // Debug.WriteLine($" TEST:: {user}");
            for (var i = 0; i < 20; i++)
            {
                User tempuser = userFaker.Generate();
                tempuser.Id = i + 1;
                tempuser.UserPicture = $"a{i}.jpg";

                // tempuser.Activities = this.GetRandomActivities(10);
                // tempuser.Tags = this.GetRandomUserTags(12);
                tempuser.ShortBio = string.Empty;
                tempuser.Password = "log";

                dummyList.Add(tempuser);
            }

            return dummyList;
        }

        public List<Activity> GenerateActivities() =>
            new List<Activity>
            {
                new Activity(1, "Running"),
                new Activity(2, "Walking"),
                new Activity(3, "Tennis"),
                new Activity(4, "Golf"),
                new Activity(5, "Padel"),
                new Activity(6, "Minigolf"),
                new Activity(7, "Camping"),
                new Activity(8, "Basketball"),
                new Activity(9, "Cycling"),
                new Activity(10, "Handball"),
                new Activity(11, "Climbing"),
                new Activity(12, "Squash"),
                new Activity(13, "Fitness"),
                new Activity(14, "Sauna"),
                new Activity(15, "Lacrosse"),
                new Activity(16, "Polo"),
                new Activity(17, "Football"),
                new Activity(18, "Yoga"),
                new Activity(19, "Dancing"),
                new Activity(20, "Pilates")
            };

        public List<Tag> GenerateTags() =>
            new List<Tag>
            {
                new Tag(1, "Outdoors"),
                new Tag(2, "Talking"),
                new Tag(3, "Wine"),
                new Tag(4, "Clubbing"),
                new Tag(5, "Travel"),
                new Tag(6, "Movies"),
                new Tag(7, "Music"),
                new Tag(8, "Larping"),
                new Tag(9, "Gaming"),
                new Tag(10, "Inked"),
                new Tag(11, "Photography"),
                new Tag(12, "Arts"),
                new Tag(13, "Polyamory"),
                new Tag(14, "Cooking"),
                new Tag(15, "Books"),
                new Tag(16, "Conspiracies"),
                new Tag(17, "FlatEarther"),
                new Tag(18, "Gambling"),
                new Tag(19, "Religion"),
                new Tag(20, "Pets"),
                new Tag(21, "Smoker"),
                new Tag(22, "Trekkie"),
                new Tag(23, "Furry"),
                new Tag(24, "Weeb"),
                new Tag(25, "Festivals")
            };

        public List<Activity> GetRandomActivities(int amount)
        {
            var rand = new Random();

            //var newList = new List<Activity>();
            List<Activity> newList = GenerateActivities();

            return newList.OrderBy(x => rand.Next()).Take(amount).ToList();
        }

        public List<Tag> GetRandomUserTags(int amount)
        {
            var rand = new Random();
            List<Tag> newList = GenerateTags();

            return newList.OrderBy(x => rand.Next()).Take(amount).ToList();
        }
    }
}