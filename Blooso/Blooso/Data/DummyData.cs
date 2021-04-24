﻿using System;
using System.Linq;

using Blooso.Interfaces;

namespace Blooso.Data
{
    #region

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Models;
    using Bogus;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public class DummyData : IDummyData
    {
        private readonly Faker<User> userFaker;

        public DummyData() => userFaker = new Faker<User>();

        public List<User> GenerateUserList()
        {
            var generatedUserList = new List<User>();
            userFaker
                .RuleFor(x => x.Id, x => x.IndexGlobal)
                .RuleFor(x => x.Name, x => x.Person.FullName)
                .RuleFor(x => x.Sex, x => x.PickRandom("Male", "Female", "Gender Fluid"))
                .RuleFor(x => x.IsVaccinated, x => x.Random.Bool())
                .RuleFor(x => x.IsInfected, x => x.Random.Bool())
                .RuleFor(x => x.UserLocation, x => x.Person.Address.City)
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth)
                .RuleFor(x => x.Activities, new List<Activity>())
                .RuleFor(x => x.Tags, new List<Tag>())
                .RuleFor(x => x.FriendList, new List<User>())
                .RuleFor(x => x.UserFeedMessages, new List<Message>());

            AddFakeDataToUsersInList(generatedUserList);

            return generatedUserList;
        }

        public List<Activity> GenerateActivities() =>
            new()
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

        public List<Tag> GenerateRandomUserTags(int amount)
        {
            var rand = new Random();
            IEnumerable<Tag> newList = GenerateTags();
            return new List<Tag>(newList.OrderBy(x => rand.Next()).Take(amount));
        }

        public List<Activity> GetRandomActivities(int amount)
        {
            var rand = new Random();
            IEnumerable<Activity> newList = GenerateActivities();
            newList = newList.OrderBy(x => rand.Next()).Take(amount).ToList();
            return newList as List<Activity>;
        }

        public List<Tag> GenerateTags() =>
            new()
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

        private void AddFakeDataToUsersInList(List<User> dummyList)
        {
            for (var i = 0; i < 20; i++)
            {
                User fakedUser = userFaker.Generate();
                fakedUser.Id = i + 1;
                fakedUser.UserPicture = $"a{i}.jpg";
                fakedUser.Activities = GetRandomActivities(10);
                fakedUser.Tags = GenerateRandomUserTags(12);
                fakedUser.ShortBio = string.Empty;
                fakedUser.Password = "log";
                dummyList.Add(fakedUser);
            }
        }

        // SEED METHODS

        #region Seed Methods

        private List<Activity> ReturnSeedActivityList(ModelBuilder modelBuilder) => GenerateActivities();

        private List<Tag> ReturnSeedTagList(ModelBuilder modelBuilder) => GenerateTags();

        private List<User> ReturnSeedUserList(ModelBuilder modelBuilder) => GenerateUserList();

        private async void ReturnSeedDatabaseAsync(ModelBuilder modelBuilder)
        {
            ReturnSeedUserList(modelBuilder);
            ReturnSeedTagList(modelBuilder);
            ReturnSeedActivityList(modelBuilder);
        }

        #endregion
    }
}