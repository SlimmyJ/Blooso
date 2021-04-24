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

    public class BloosoContext : DbContext
    {
        public BloosoContext()
        {
            Database.EnsureCreated();
        }

        protected virtual IDummyData FakerBro => new DummyData();

        public DbSet<Message> Messages { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserLocation> UserLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapSqlMethod(modelBuilder);

            modelBuilder.Entity<Activity>().HasData(Activities);
            modelBuilder.Entity<Tag>().HasData(Tags);
            modelBuilder.Entity<User>().HasData(Users);
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
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Blooso26.sqlite");
            optionsBuilder.UseSqlite($"FileName = {dbPath}");
            optionsBuilder.EnableSensitiveDataLogging(true);
        }
    }
}