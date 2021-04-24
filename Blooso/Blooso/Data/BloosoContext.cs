using System;
using System.ComponentModel.DataAnnotations;

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

        public static IDummyData FakerBro => new DummyData();

        public DbSet<Message> Messages { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserLocation> UserLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapSqlMethod(modelBuilder);
            SeedDatabase(modelBuilder);
        }

        private static void MapSqlMethod(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(x => x.FriendList);
            modelBuilder.Entity<User>().HasMany(x => x.UserFeedMessages);
            modelBuilder.Entity<User>().HasMany(x => x.Tags);
            modelBuilder.Entity<User>().HasMany(x => x.Activities);

            modelBuilder.Entity<Tag>().HasMany(x => x.TagUsers);
            modelBuilder.Entity<Activity>().HasMany(x => x.ActivityUsers);
            modelBuilder.Entity<Message>().HasOne(x => x.Author);
            modelBuilder.Entity<Message>().HasOne(x => x.Recipient);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Blooso22.sqlite");
            optionsBuilder.UseSqlite($"FileName = {dbPath}");
        }

        // SEED METHODS

        #region Seed Methods

        private void SeedActivityList(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>().HasData(FakerBro.GenerateActivities());
        }

        private void SeedTagList(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasData(FakerBro.GenerateTags());
        }

        private void SeedUserList(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(FakerBro.GenerateUserList());
        }

        private void SeedDatabase(ModelBuilder modelBuilder)
        {
            SeedUserList(modelBuilder);
            SeedTagList(modelBuilder);
            SeedActivityList(modelBuilder);
        }

        #endregion
    }
}