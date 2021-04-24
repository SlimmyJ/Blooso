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
        public DummyData FakerBro;

        public BloosoContext()
        {
            Database.EnsureCreated();
        }

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
            modelBuilder.Entity<User>().HasMany(x => x.Activities);

            modelBuilder.Entity<Tag>().HasMany(x => x.Users);
            modelBuilder.Entity<Activity>().HasMany(x => x.Users);
            modelBuilder.Entity<Message>().HasOne(x => x.Author);
            modelBuilder.Entity<Message>().HasOne(x => x.Recipient);

            // Seed Data
            SeedUserList(modelBuilder);
            SeedTagList(modelBuilder);
            SeedActivityList(modelBuilder);

            // TODO: Seed Users
            // TODO: Fix relations with Activity and Tags
        }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Blooso11.sqlite");
            optionsBuilder.UseSqlite($"FileName = {dbPath}");
        }
    }
}