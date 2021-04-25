using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
            MapSqlMethod(modelBuilder);
            modelBuilder.Entity<Activity>().HasData(FakerBro.GenerateActivities());
            modelBuilder.Entity<Tag>().HasData(FakerBro.GenerateTags());
            modelBuilder.Entity<User>().HasData(FakerBro.GenerateUserList());
        }

        private static void MapSqlMethod(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(x => x.Activities);
            modelBuilder.Entity<User>().HasMany(x => x.UserFeedMessages);
            modelBuilder.Entity<User>().HasMany(x => x.FriendList);
            modelBuilder.Entity<User>().HasMany(x => x.Tags);

            modelBuilder.Entity<Tag>().HasOne(x => x.TagUser);
            modelBuilder.Entity<Activity>().HasOne(x => x.ActivityUser);
            modelBuilder.Entity<Message>().HasOne(x => x.Author);
            modelBuilder.Entity<Message>().HasOne(x => x.Recipient);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Blooso27.sqlite");
            optionsBuilder.UseSqlite($"FileName = {dbPath}");
            optionsBuilder.EnableSensitiveDataLogging(true);
        }
    }
}