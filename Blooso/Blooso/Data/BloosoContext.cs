using Blooso.Models;

using Microsoft.EntityFrameworkCore;

using System.IO;

using Xamarin.Essentials;

namespace Blooso.Data
{
    public sealed class BloosoContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public DbSet<Models.Tag> Tags { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserLocation> UserLocations { get; set; }

        public BloosoContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map relations
            modelBuilder.Entity<User>().HasMany(x => x.FriendList);
            modelBuilder.Entity<User>().HasMany(x => x.UserFeedMessages);

            modelBuilder.Entity<User>().HasMany(x => x.Tags);
            modelBuilder.Entity<Models.Tag>().HasMany(x => x.Users);
            modelBuilder.Entity<Activity>().HasMany(x => x.Users);
            modelBuilder.Entity<User>().HasMany(x => x.Activities);

            modelBuilder.Entity<Message>().HasOne(x => x.Author);
            modelBuilder.Entity<Message>().HasOne(x => x.Recipient);

            // Seed Data
            SeedTags(modelBuilder);
            SeedData(modelBuilder);

            // TODO: Seed Users
            // TODO: Fix relations with Activity and Tags
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>().HasData(
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
                new Activity(20, "Pilates"));
        }

        private void SeedTags(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Tag>().HasData(
                new Models.Tag(1, "Outdoors"),
                new Models.Tag(2, "Talking"),
                new Models.Tag(3, "Wine"),
                new Models.Tag(4, "Clubbing"),
                new Models.Tag(5, "Travel"),
                new Models.Tag(6, "Movies"),
                new Models.Tag(7, "Music"),
                new Models.Tag(8, "Larping"),
                new Models.Tag(9, "Gaming"),
                new Models.Tag(10, "Inked"),
                new Models.Tag(11, "Photography"),
                new Models.Tag(12, "Arts"),
                new Models.Tag(13, "Polyamory"),
                new Models.Tag(14, "Cooking"),
                new Models.Tag(15, "Books"),
                new Models.Tag(16, "Conspiracies"),
                new Models.Tag(17, "FlatEarther"),
                new Models.Tag(18, "Gambling"),
                new Models.Tag(19, "Religion"),
                new Models.Tag(20, "Pets"),
                new Models.Tag(21, "Smoker"),
                new Models.Tag(22, "Trekkie"),
                new Models.Tag(23, "Furry"),
                new Models.Tag(24, "Weeb"),
                new Models.Tag(25, "Festivals"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Blooso5.sqlite");
            optionsBuilder.UseSqlite($"FileName = {dbPath}");
        }
    }
}