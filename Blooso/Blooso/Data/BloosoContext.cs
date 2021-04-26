#region

using System.Collections.Generic;
using System.IO;

using Blooso.Models;

using Microsoft.EntityFrameworkCore;

using Xamarin.Essentials;

#endregion

namespace Blooso.Data
{
    public sealed class BloosoContext : DbContext
    {
        public BloosoContext()
        {
            Database.EnsureCreated();
        }

        private static DummyData FakerBro => DummyData.CreateInstance();

        public DbSet<Message> Messages { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map relations
            modelBuilder.Entity<User>().HasMany(x => x.FriendList);
            modelBuilder.Entity<User>().HasMany(x => x.UserFeedMessages);
            modelBuilder.Entity<User>().OwnsMany<Message>(x => x.UserFeedMessages);
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
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Blooso49.sqlite");
            optionsBuilder.UseSqlite($"FileName = {dbPath}");
        }

        // SEED METHODS

        #region Seed Methods

        private static List<Activity> ReturnSeedActivityList(ModelBuilder modelBuilder) =>
            FakerBro.GenerateActivities();

        private static List<Tag> ReturnSeedTagList(ModelBuilder modelBuilder) => FakerBro.GenerateTags();

        private static List<User> ReturnSeedUserList(ModelBuilder modelBuilder) => FakerBro.GenerateUserList();

        #endregion
    }
}