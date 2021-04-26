using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Sqlite.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace Blooso.Data
{
    #region

    using System.Collections.Generic;
    using System.IO;
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Xamarin.Essentials;

    #endregion

    public sealed class BloosoContext : DbContext
    {
        public BloosoContext()
        {
            Database.EnsureCreated();
        }

        private IDummyData FakerBro => new DummyData();

        public DbSet<Message> Messages { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserLocation> UserLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map relations
            modelBuilder.Entity<User>().HasMany(x => x.FriendList);
            modelBuilder.Entity<User>().HasMany(x => x.UserFeedMessages);

            modelBuilder.Entity<User>().HasMany(x => x.Tags);
            modelBuilder.Entity<Tag>().HasMany(x => x.TagUsers);
            modelBuilder.Entity<Activity>().HasMany(x => x.ActivityUsers);
            modelBuilder.Entity<User>().HasMany(x => x.Activities);

            modelBuilder.Entity<Message>().HasOne(x => x.Author);
            modelBuilder.Entity<Message>().HasOne(x => x.Recipient);

            // Seed Data
            ReturnSeedUserList(modelBuilder);
            ReturnSeedTagList(modelBuilder);
            ReturnSeedActivityList(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Blooso44.sqlite");
            optionsBuilder.UseSqlite($"FileName = {dbPath}");
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        // SEED METHODS

        #region Seed Methods

        private List<Activity> ReturnSeedActivityList(ModelBuilder modelBuilder) => FakerBro.GenerateActivities();

        private List<Tag> ReturnSeedTagList(ModelBuilder modelBuilder) => FakerBro.GenerateTags();

        private List<User> ReturnSeedUserList(ModelBuilder modelBuilder) => FakerBro.GenerateUserList();

        #endregion
    }
}